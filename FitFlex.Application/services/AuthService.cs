using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.User_dto;
using FitFlex.Application.Interfaces;
using FitFlex.Application.Repository_interface;
using FitFlex.Domain.Entities.Users_Model;
using FitFlex.Domain.Enum;
using Microsoft.IdentityModel.Tokens;


namespace FitFlex.Application.services
{
    public class AuthService : IAuth
    {
        private readonly IUserRepository repo;
        public AuthService(IUserRepository auth)
        {
            repo = auth;
        }

        public async Task<User> GetByUSer( string Email)
        {
            var user = await repo.AllUsers();
            var logindata = user.FirstOrDefault(p => p.Email == Email);

            if (logindata is null) return null;
            var byid = new User
            {
                UserID = logindata.UserID,
                UserName = logindata.UserName,
                Email = logindata.Email,
                Role = logindata.Role
            };
            
            return byid;

        }

        public async Task<LoginResponseDto> Login(LoginDto dto)
        {
            var users = await repo.AllUsers();
            var loginData = users.FirstOrDefault(p => p.Email == dto.Email);
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
            var exist = await repo.AllUsers();
            if (exist.Any(o => o.Email == dto.Email))return "user already exist";
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = dto.Password,
                created = dto.created,
                Role = UserRole.user

            };
            await repo.AddUserAsync(user);
            await repo.SaveChangesAsync();
            return "registration sucess";
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
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
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
