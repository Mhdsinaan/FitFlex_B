using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.Interfaces;
using FitFlex.Domain.Entities.Subscription_model;
using FitFlex.Infrastructure.Interfaces;

namespace FitFlex.Application.services
{
    public class SubscriptionService : ISubscription
    {
        private readonly IRepository<SubscriptionPlan> _subscription;
        public SubscriptionService(IRepository<SubscriptionPlan> subscription)
        {
            _subscription = subscription;
        }
        public async Task<SubscriptionPlan?> CreatePlanAsync(SubscriptionPlan plan)
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
                DurationInDays = plan.DurationInDays,
                CreatedOn = DateTime.UtcNow,
               
            };

           _subscription.AddAsync(newPlan);
            await _subscription.SaveChangesAsync();

            return newPlan;
        }


        public async Task<bool> DeletePlanAsync(int id)
        {
            var plan = await _subscription.GetByIdAsync(id);
            if (plan == null) return false;

            _subscription.Delete(plan);
            await _subscription.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SubscriptionPlan>> GetAllPlansAsync()
        {
            return await _subscription.GetAllAsync();
        }

        public async Task<SubscriptionPlan?> GetPlanByIdAsync(int id)
        {
            return await _subscription.GetByIdAsync(id);
        }
        public async Task<SubscriptionPlan?> UpdatePlanAsync(int id, SubscriptionPlan plan)
        {
            var existing = await _subscription.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = plan.Name;
            existing.Description = plan.Description;
            existing.Price = plan.Price;
            existing.DurationInDays = plan.DurationInDays;
            existing.ModifiedOn = DateTime.UtcNow;

            _subscription.Update(existing);
            await _subscription.SaveChangesAsync();

            return existing;
        }
    }
}

