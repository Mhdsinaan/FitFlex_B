using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s.Trainers_dto;
using FitFlex.Application.Interfaces;
using FitFlex.CommenAPi;
using FitFlex.Domain.Entities.Trainer_model;
using FitFlex.Domain.Entities.Users_Model;
using FitFlex.Domain.Enum;
using FitFlex.Infrastructure.Interfaces;

namespace FitFlex.Application.services
{
    public class TrainerService : ITrainerservice
    {
        private readonly IRepository<Trainer> _trainerRepo;
        private readonly IRepository<User> _userRepo;

        public TrainerService(IRepository<Trainer> trainerRepo, IRepository<User> userRepo)
        {
            _trainerRepo = trainerRepo;
            _userRepo = userRepo;
        }

            public async Task<APiResponds<TrainerResponseDto?>> UpdateTrainerStatusAsync(int trainerId, TrainerStatus newStatus)
            {
                try
                {
                    var trainer = await _trainerRepo.GetByIdAsync(trainerId);
                    if (trainer == null || trainer.IsDelete)
                        return new APiResponds<TrainerResponseDto?>("404", "Trainer not found", null);

            

               
                    trainer.status = newStatus;
                    trainer.ModifiedOn = DateTime.UtcNow;

                    _trainerRepo.Update(trainer);
                    await _trainerRepo.SaveChangesAsync();

                    var dto = new TrainerResponseDto
                    {
                        Id = trainer.Id,
                        FullName = trainer.FullName,
                        Email = trainer.Email,
                        PhoneNumber = trainer.PhoneNumber,
                        Gender = trainer.Gender,
                        ExperienceYears = trainer.ExperienceYears,
                        Status = trainer.status,
                        CreatedOn = trainer.CreatedOn
                    };

                    return new APiResponds<TrainerResponseDto?>("200", $"Trainer {newStatus} successfully", dto);
                }
                catch (Exception ex)
                {
                    return new APiResponds<TrainerResponseDto?>("500", ex.Message, null);
                }
            }

        public async Task<APiResponds<User?>> DeleteTrainerAsync(int trainerId, int currentUserId)
        {
            try
            {
                var trainer = await _trainerRepo.GetByIdAsync(trainerId);
                if (trainer == null || trainer.UserId == 0 || trainer.IsDelete)
                    return new APiResponds<User?>("404", "Trainer not found or already deleted", null);

                var user = await _userRepo.GetByIdAsync(trainer.UserId);
                if (user == null)
                    return new APiResponds<User?>("404", "Associated user not found", null);

                trainer.DeletedBy = currentUserId;
                trainer.DeletedOn = DateTime.UtcNow;
                trainer.IsDelete = true;
                user.IsDelete = true;

                _trainerRepo.Update(trainer);
                _userRepo.Update(user);

                await _trainerRepo.SaveChangesAsync();
                await _userRepo.SaveChangesAsync();

                return new APiResponds<User?>("200", "Trainer deleted successfully", user);
            }
            catch (Exception ex)
            {
                return new APiResponds<User?>("500", ex.Message, null);
            }
        }

        public async Task<APiResponds<List<TrainerResponseDto>>> GetAllTrainersAsync()
        {
            try
            {
                var trainers = (await _trainerRepo.GetAllAsync()).Where(t => !t.IsDelete).ToList();

                var dtoList = trainers.Select(t => new TrainerResponseDto
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    Gender = t.Gender,
                    ExperienceYears = t.ExperienceYears,
                    CreatedOn = t.CreatedOn
                }).ToList();

                return new APiResponds<List<TrainerResponseDto>>("200", "Trainers fetched successfully", dtoList);
            }
            catch (Exception ex)
            {
                return new APiResponds<List<TrainerResponseDto>>("500", ex.Message, null);
            }
        }

        public async Task<APiResponds<TrainerResponseDto>> GetTrainerByIdAsync(int trainerId)
        {
            try
            {
                var trainer = await _trainerRepo.GetByIdAsync(trainerId);
                if (trainer == null || trainer.IsDelete)
                    return new APiResponds<TrainerResponseDto>("404", "Trainer not found", null);

                var dto = new TrainerResponseDto
                {
                    Id = trainer.Id,
                    Email = trainer.Email,
                    FullName = trainer.FullName,
                    Gender = trainer.Gender,
                    PhoneNumber = trainer.PhoneNumber,
                    ExperienceYears = trainer.ExperienceYears,
                  
                };

                return new APiResponds<TrainerResponseDto>("200", "Trainer fetched successfully", dto);
            }
            catch (Exception ex)
            {
                return new APiResponds<TrainerResponseDto>("500", ex.Message, null);
            }
        }

        public async Task<APiResponds<bool>> UpdateTrainerAsync(int trainerId, TrainerUpdateDto dto)
        {
            try
            {
                var trainer = await _trainerRepo.GetByIdAsync(trainerId);
                if (trainer == null)
                    return new APiResponds<bool>("404", "Trainer not found", false);

                trainer.FullName = dto.FullName;
                trainer.PhoneNumber = dto.PhoneNumber;
                trainer.Gender = dto.Gender;
                trainer.ExperienceYears = dto.ExperienceYears;

                _trainerRepo.Update(trainer);
                await _trainerRepo.SaveChangesAsync();

                return new APiResponds<bool>("200", "Trainer updated successfully", true);
            }
            catch (Exception ex)
            {
                return new APiResponds<bool>("500", ex.Message, false);
            }
        }
    }
}
