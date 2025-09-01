using FitFlex.Domain.Entities.Subscription_model;
using FitFlex.Infrastructure.Interfaces;

namespace FitFlex.Application.Interfaces
{
    public interface ISubscription
    {
       
        Task<IEnumerable<SubscriptionPlan>> GetAllPlansAsync();

        Task<SubscriptionPlan?> GetPlanByIdAsync(int id);

      
        Task<SubscriptionPlan> CreatePlanAsync(SubscriptionPlan plan);

       
        Task<SubscriptionPlan?> UpdatePlanAsync(int id, SubscriptionPlan plan);


        Task<bool> DeletePlanAsync(int id);
    }
}
