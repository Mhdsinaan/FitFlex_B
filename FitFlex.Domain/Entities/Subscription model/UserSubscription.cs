using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Domain.Enum;
using Microsoft.Identity.Client.AppConfig;

namespace FitFlex.Domain.Entities.Subscription_model
{
    public class UserSubscription

    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public subscriptionStatus status{ get; set; }
        public PaymentStatus PaymentStatus { get; set; }

    }
}
