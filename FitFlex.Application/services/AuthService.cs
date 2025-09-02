using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks; 
using FitFlex.Application.DTO_s.Trainers_dto;
using FitFlex.Application.DTO_s.User_dto;
using FitFlex.Application.Interfaces;
using FitFlex.Domain.Entities.Trainer_model;
using FitFlex.Domain.Entities.Users_Model;
using FitFlex.Domain.Enum;
using FitFlex.Infrastructure.Interfaces;
using FitFlex.Infrastructure.Migrations;
using Microsoft.IdentityModel.Tokens;


namespace FitFlex.Application.services
{
    public class AuthService : IAuth
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Trainer> _trainerRepo;
        public AuthService(IRepository<User> auth, IRepository<Trainer> Trainer)
        {
            _userRepo = auth;
            _trainerRepo = Trainer;
        }

        

        public async Task<UserResponseDto> GetByUSer(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            


            if (user is null)
                return null;
            if (user.IsDelete) return null;

            var dto = new UserResponseDto
            {
                Id = user.ID,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return dto;
        }


        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            if (users == null || !users.Any())

                return new List<UserResponseDto>();
            var userDtos = users.Select(u => new UserResponseDto
            {
                Id = u.ID,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role.ToString()
            }).ToList();

            return userDtos;
        }

        public async Task<TrainerResponseDto> GetTrainerByID(int id)
        {
            var trainer = await _trainerRepo.GetByIdAsync(id);
            if (trainer.IsDelete) return null;
            if (trainer is null) return null;

            return new TrainerResponseDto
            {
                Id = trainer.Id,
                FullName = trainer.FullName,
                PhoneNumber = trainer.PhoneNumber,
                Gender = trainer.Gender,
                
                ExperienceYears = trainer.ExperienceYears
            };
        }

        public async Task<LoginResponseDto> Login(LoginDto dto)
        {
            var users = await _userRepo.GetAllAsync();
            var loginData = users.FirstOrDefault(p => p.Email == dto.Email && p.Password == dto.Password && p.IsDelete==false);
            if (loginData is null) return null;

            var token = createToken(loginData);
            return new LoginResponseDto
            {
                Email = loginData.Email,
                UserName = loginData.UserName,
                Role = loginData.Role.ToString(),
                Token = token

            };



        }

        public async Task<string> Register(RegisterDto dto)
        {
            
            var exist = await _userRepo.GetAllAsync();
            if (exist.Any(o => o.Email == dto.Email)) return "user already exist";
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = dto.Password,
                Role = UserRole.user,
                  
                


            };
            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();
            return "registration success";
        }

        public async Task<string> TrainerRegistration(TrainerRegisterDto dto)
        {
          
            var trainer = (await _userRepo.GetAllAsync())
                            .FirstOrDefault(p => p.Email == dto.Email);

            if (trainer != null)
            {
                return "trainer already exists";
            }

            var newUSER = new User
            {
                UserName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password, 
                Role = UserRole.Trainer
            };

            await _userRepo.AddAsync(newUSER);
            await _userRepo.SaveChangesAsync();

           
            var newTrainer = new Trainer()
            {
                FullName = dto.FullName,
                ExperienceYears = dto.ExperienceYears,
                Email = dto.Email,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
                
               
               
                UserId = newUSER.ID
            };

            await _trainerRepo.AddAsync(newTrainer);
            await _trainerRepo.SaveChangesAsync();

            return "new trainer created successfully";
        }



        private string createToken(User user)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("muhammedsinandotnetdeveloperatbridgeon");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Audience = "myusers",
                Issuer = "MyApp",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)


            };
            var token = TokenHandler.CreateToken(tokenDescriptor);
            return TokenHandler.WriteToken(token);

        }

        
    }

}
