using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.Trainers_dto;
using FitFlex.Application.Interfaces;
using FitFlex.Domain.Entities.Trainer_model;
using FitFlex.Infrastructure.Interfaces;

namespace FitFlex.Application.services
{
    public class TrainerService : ITrainerservice
    {
        private readonly IRepository<Trainer> _trainerRepo;
        public TrainerService(IRepository<Trainer> trainerrepo)
        {
            _trainerRepo = trainerrepo;
        }

        public async Task<bool> DeleteTrainerAsync(int trainerID)
        {
            var user = await _trainerRepo.GetByIdAsync(trainerID);
            if (user is null) return false;
            _trainerRepo.Delete(user);
            return true;
            
        }

        public async Task<List<TrainerResponseDto>> GetAllTrainersAsync()
        {
            var trainers = await _trainerRepo.GetAllAsync();

            if (trainers == null || !trainers.Any())
                return new List<TrainerResponseDto>();

            var response = trainers.Select(t => new TrainerResponseDto
            {
                Id = t.Id,
                FullName = t.FullName,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber,
                Gender = t.Gender,
                ExperienceYears=t.ExperienceYears,
              
                CreatedOn = t.CreatedOn,
                
            }).ToList();

            return response;
        }



        public async Task<TrainerResponseDto> GetTrainerByIdAsync(int trainerId)
        {
            var user = await _trainerRepo.GetByIdAsync(trainerId);
            if (user is null) return null;
            return new TrainerResponseDto()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber


            };
        }

        public Task<bool> UpdateTrainerAsync(int trainerId, TrainerUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
