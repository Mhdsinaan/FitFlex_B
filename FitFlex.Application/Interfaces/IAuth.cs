using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.Trainers_dto;
using FitFlex.Application.DTO_s.User_dto;

using FitFlex.Domain.Entities.Trainer_model;
using FitFlex.Domain.Entities.Users_Model;

namespace FitFlex.Application.Interfaces
{
    public interface IAuth
    {
        
        Task<string> Register(RegisterDto dto);
        Task<LoginResponseDto> Login(LoginDto dto);
        Task<UserResponseDto> GetByUSer(int id);
        Task<string> TrainerRegistration(TrainerRegisterDto dto);


        Task<List<UserResponseDto>> GetAllAsync();
        Task<Trainer> GetTrainerByID(int id);
        

    }
}
