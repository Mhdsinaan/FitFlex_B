using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.Trainers_dto;

namespace FitFlex.Application.Interfaces
{
    public interface ITrainerservice
    {
        Task<List<TrainerResponseDto>> GetAllTrainersAsync();
        Task<TrainerResponseDto> GetTrainerByIdAsync(int trainerId);
        Task<bool> UpdateTrainerAsync(int trainerId, TrainerUpdateDto dto);
        Task<bool> DeleteTrainerAsync(int trainerID);
    }
}
