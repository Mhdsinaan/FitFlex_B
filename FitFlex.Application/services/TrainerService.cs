using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.Trainers_dto;
using FitFlex.Application.Interfaces;
using FitFlex.Domain.Entities.Trainer_model;
using FitFlex.Domain.Entities.Users_Model;
using FitFlex.Infrastructure.Interfaces;

namespace FitFlex.Application.services
{
    public class TrainerService : ITrainerservice
    {
        private readonly IRepository<Trainer> _trainerRepo;
        private readonly IRepository<User> _UserRepo;
        public TrainerService(IRepository<Trainer> trainerrepo, IRepository<User> UserRepo)
        {
            _trainerRepo = trainerrepo;
            _UserRepo = UserRepo;
        }

        public async Task<User?> DeleteTrainerAsync(int trainerId, int currentUserId)
        {
           
            var trainer = await _trainerRepo.GetByIdAsync(trainerId);
            if (trainer == null)
            {
              
                return null;
            }

           
            if (trainer.UserId == 0)
            {
               
                return null;
            }

           
            var user = await _UserRepo.GetByIdAsync(trainer.UserId);
            if (user == null)
            {
               
                return null;
            }

           
            if (trainer.IsDelete == true)
            {
                return null;
            }

           
            trainer.DeletedBy = currentUserId;
            trainer.DeletedOn = DateTime.UtcNow;
            trainer.IsDelete = true;
            user.IsDelete = true;

            _trainerRepo.Update(trainer);

           
            _UserRepo.Delete(user);

          
            await _trainerRepo.SaveChangesAsync();
            await _UserRepo.SaveChangesAsync();

            return user;
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
            if (user.IsDelete == false)
            {
                return new TrainerResponseDto()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                ExperienceYears = user.ExperienceYears,
                Status = user.Status
                


            };
            }
            return null;
        }

        public async Task<bool> UpdateTrainerAsync(int trainerId, TrainerUpdateDto dto)
        {
          
            var trainer = await _trainerRepo.GetByIdAsync(trainerId);
            if (trainer == null)
            {
                return false; 
            }

            
            trainer.FullName = dto.FullName;
            trainer.PhoneNumber = dto.PhoneNumber;
            trainer.Gender = dto.Gender;
            trainer.ExperienceYears = dto.ExperienceYears;

           
             _trainerRepo.Update(trainer);
            await _trainerRepo.SaveChangesAsync();

            return true;
        }

    }
}
