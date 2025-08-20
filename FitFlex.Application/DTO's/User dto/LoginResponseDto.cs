using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Domain.Enum;

namespace FitFlex.Application.DTO_s.User_dto
{
    public class LoginResponseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserRole role { get; set; }

        public string Token { get; set; }
    }
}
