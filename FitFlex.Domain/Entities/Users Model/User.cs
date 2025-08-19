using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Domain.Enum;

namespace FitFlex.Domain.Entities.Users_Model
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; } 
        public DateTime created { get; set; }

    }
}
