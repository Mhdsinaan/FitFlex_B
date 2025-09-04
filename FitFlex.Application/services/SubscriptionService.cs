using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitFlex.Application.DTO_s.subscriptionDto;
using FitFlex.Application.Interfaces;
using FitFlex.Domain.Entities.Subscription_model;
using FitFlex.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace FitFlex.Application.services
{
    public class SubscriptionService : ISubscription
    {
        private readonly IRepository<SubscriptionPlan> _subscription;
        private readonly IMapper _mapper;

        public SubscriptionService(IRepository<SubscriptionPlan> subscription, IMapper mapper)
        {
            _subscription = subscription;
            _mapper = mapper;
        }
        public async Task<SubscriptionPlansResponseDto?> CreatePlanAsync(SubscriptionPlanDto plan)
        {
          
            var subscriptions = await _subscription.GetAllAsync();
            var existing = subscriptions.FirstOrDefault(p => p.Name == plan.Name);

            if (existing != null)
                return null;

            
            var newPlan = new SubscriptionPlan
            {
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationInDays = plan.DurationInDays
            };

            await _subscription.AddAsync(newPlan);
            await _subscription.SaveChangesAsync();

          
            var response = new SubscriptionPlansResponseDto
            {
                Id = newPlan.Id,
                Name = newPlan.Name,
                Description = newPlan.Description,
                Price = newPlan.Price,
                DurationInDays = newPlan.DurationInDays
            };

            return response;
        }



        public async Task<bool> DeletePlanAsync(int id)
        {
            var plans= await _subscription.GetAllAsync();
            var plan = plans.FirstOrDefault(p => p.Id == id && p.IsDelete == false);

            if (plan == null) return false;

            plan.IsDelete = true;
            await _subscription.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<SubscriptionPlansResponseDto>> GetAllPlansAsync()
        {
            var plans = await _subscription.GetAllAsync();

            if (plans == null )
                return Enumerable.Empty<SubscriptionPlansResponseDto>();

           
            var planDtos = _mapper.Map<IEnumerable<SubscriptionPlansResponseDto>>(plans);
            return planDtos;
        }


        public async Task<SubscriptionPlan?> GetPlanByIdAsync(int id)
        {
            return await _subscription.GetByIdAsync(id);
        }
        public async Task<SubscriptionPlansResponseDto?> UpdatePlanAsync(int id, SubscriptionPlanDto planDto)
        {
            var existing = await _subscription.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = planDto.Name;
            existing.Description = planDto.Description;
            existing.Price = planDto.Price;
            existing.DurationInDays = planDto.DurationInDays;
            existing.ModifiedOn = DateTime.UtcNow;

            _subscription.Update(existing);
            await _subscription.SaveChangesAsync();

            return _mapper.Map<SubscriptionPlansResponseDto>(existing); 
        }


    }
}

