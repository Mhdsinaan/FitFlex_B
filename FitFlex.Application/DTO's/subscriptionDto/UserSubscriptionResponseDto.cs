using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitFlex.Application.DTO_s.subscriptionDto
{
    public class UserSubscriptionResponseDto
    {
        public int SubscriptionId { get; set; }
        public string Status { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
