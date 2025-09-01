using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.Trainers_dto;
using FitFlex.Domain.Entities.Trainer_model;
using FitFlex.Domain.Entities.Users_Model;

namespace FitFlex.Application.Interfaces
{
    public interface ITrainerservice
    {
        Task<List<TrainerResponseDto>> GetAllTrainersAsync();
        Task<TrainerResponseDto> GetTrainerByIdAsync(int trainerId);
        Task<bool> UpdateTrainerAsync(int trainerId, TrainerUpdateDto dto);
        Task<User?> DeleteTrainerAsync(int trainerId);
    }
}
