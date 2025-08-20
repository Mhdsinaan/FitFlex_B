using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Domain.Entities.Users_Model;

namespace FitFlex.Application.Repository_interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByID(int userId);
        Task<List<User>> AllUsers();
        Task AddUserAsync(User user);
        Task SaveChangesAsync();



    }
}
