using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.User_dto;
using FitFlex.Application.Repository_interface;
using FitFlex.Domain.Entities.Users_Model;

namespace FitFlex.Application.Interfaces
{
    public interface IAuth
    {
        Task<string> Register(RegisterDto dto);
        Task<LoginResponseDto> Login(LoginDto dto);
        Task<User> GetByUSer(string Email);

    }
}
