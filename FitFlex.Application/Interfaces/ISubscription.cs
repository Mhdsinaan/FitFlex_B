using FitFlex.Application.DTO_s.subscriptionDto;
using FitFlex.Domain.Entities.Subscription_model;
using FitFlex.Infrastructure.Interfaces;

namespace FitFlex.Application.Interfaces
{
    public interface ISubscription
    {

        Task<IEnumerable<SubscriptionPlansResponseDto>> GetAllPlansAsync();
        Task<SubscriptionPlan?> GetPlanByIdAsync(int id);
        Task<SubscriptionPlansResponseDto> CreatePlanAsync(SubscriptionPlanDto plan);
        Task<SubscriptionPlansResponseDto?> UpdatePlanAsync(int id, SubscriptionPlanDto planDto);
        Task<bool> DeletePlanAsync(int id);
        
    }


}
