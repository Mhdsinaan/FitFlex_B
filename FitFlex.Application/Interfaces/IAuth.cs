using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.User_dto;
using FitFlex.Application.Repository_interface;

namespace FitFlex.Application.Interfaces
{
    public interface IAuth
    {
        Task<string> Register(RegisterDto dto);
        Task<LoginResponseDto> Login(LoginResponseDto dto);

    }
}
