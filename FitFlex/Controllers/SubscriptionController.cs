using FitFlex.Application.Interfaces;
using FitFlex.CommenAPi;
using FitFlex.Domain.Entities.Subscription_model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitFlex.API.Controllers.Admin
{
    [Route("api/admin/plans")]
    [ApiController]
    //[Authorize(Roles = "Admin")] // Only admin can access
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscription _subscriptionService;

        public SubscriptionController(ISubscription subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // GET: api/admin/plans
        [HttpGet]
        public async Task<IActionResult> GetAllPlans()
        {
            var plans = await _subscriptionService.GetAllPlansAsync();
            return Ok(new APiResponds<IEnumerable<SubscriptionPlan>>("200", "Plans fetched successfully", plans));
        }

        // GET: api/admin/plans/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanById(int id)
        {
            var plan = await _subscriptionService.GetPlanByIdAsync(id);
            if (plan == null)
                return NotFound(new APiResponds<string>("404", "Plan not found", null));

            return Ok(new APiResponds<SubscriptionPlan>("200", "Plan fetched successfully", plan));
        }

        // POST: api/admin/plans
        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] SubscriptionPlan plan)
        {
            var createdPlan = await _subscriptionService.CreatePlanAsync(plan);
            if (createdPlan == null)
                return Conflict(new APiResponds<string>("409", "Plan with the same name already exists", null));

            return Ok(new APiResponds<SubscriptionPlan>("200", "Plan created successfully", createdPlan));
        }

        // PUT: api/admin/plans/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlan(int id, [FromBody] SubscriptionPlan plan)
        {
            var updatedPlan = await _subscriptionService.UpdatePlanAsync(id, plan);
            if (updatedPlan == null)
                return NotFound(new APiResponds<string>("404", "Plan not found", null));

            return Ok(new APiResponds <SubscriptionPlan>("200", "Plan updated successfully", updatedPlan));
        }

        // DELETE: api/admin/plans/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            var deleted = await _subscriptionService.DeletePlanAsync(id);
            if (!deleted)
                return NotFound(new APiResponds<string>("404", "Plan not found", null));

            return Ok(new APiResponds<string>("200", "Plan deleted successfully", null));
        }
    }
}
