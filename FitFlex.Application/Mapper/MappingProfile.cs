using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FitFlex.Application.DTO_s.subscriptionDto;
using FitFlex.Domain.Entities.Subscription_model;
using FitFlex.Infrastructure.Migrations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitFlex.Application.Mapper
{
    public  class MappingProfile: Profile
    {
       public MappingProfile()
        {
            CreateMap<SubscriptionPlan, SubscriptionPlanDto>().ReverseMap();
            CreateMap<SubscriptionPlan, SubscriptionPlansResponseDto>().ReverseMap();
        }
    }
}
