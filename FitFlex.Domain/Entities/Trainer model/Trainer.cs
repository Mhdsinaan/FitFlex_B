using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.DTO_s;
using FitFlex.Domain.Entities.Users_Model;

namespace FitFlex.Domain.Entities.Trainer_model
{
    public class Trainer:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }         
        public string Email { get; set; }             
        public string PhoneNumber { get; set; }           
        public string Gender { get; set; }          
        public int ExperienceYears { get; set; }        
        public DateTime JoiningDate { get; set; }
        public string Status { get; set; } = "Pending";
        
      

  


    }
}
