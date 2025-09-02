﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s;
using FitFlex.Domain.Entities.Trainer_model;
using FitFlex.Domain.Enum;

namespace FitFlex.Domain.Entities.Users_Model
{
    public class User:BaseEntity
    {
        public int ID { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        //public bool IsDelete { get; set; } = false;

        //public Trainers Trainer { get; set; }


    }
}
