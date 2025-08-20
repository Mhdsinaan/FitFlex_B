using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.User_dto;
using FitFlex.Application.Interfaces;
using FitFlex.Application.Repository_interface;
using FitFlex.Domain.Entities.Users_Model;


namespace FitFlex.Application.services
{
    public class AuthService : IAuth
    {
        private readonly IUserRepository repo;
        public AuthService(IUserRepository auth)
        {
            repo = auth;
        }
        public async Task<LoginResponseDto> Login(LoginResponseDto dto)
        {
            var users = await repo.AllUsers();
            var loginData = users.FirstOrDefault(p => p.Email == dto.Email);
            if (loginData is null) return null;
            return new LoginResponseDto
            {
                Email = loginData.Email,
                UserName = loginData.UserName,
                
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
                created = dto.created

            };
            await repo.AddUserAsync(user);
            await repo.SaveChangesAsync();
            return "registration sucess";
        }
        

    }

}
