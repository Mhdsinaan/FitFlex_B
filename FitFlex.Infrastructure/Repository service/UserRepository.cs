using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitFlex.Application.Repository_interface;
using FitFlex.Domain.Entities.Users_Model;
using FitFlex.Infrastructure.Db_context;
using Microsoft.EntityFrameworkCore;

namespace FitFlex.Infrastructure.Repository_service
{
    public class UserRepository : IUserRepository
    {
        private readonly MyContext _myContext;
        public UserRepository(MyContext context)
        {
            _myContext = context;
            
        }

        public async Task AddUserAsync(User user)
        {
            var userdata = await _myContext.AddAsync(user);
        }

        public async Task<List<User>> AllUsers()
        {
            var AllUsers = await _myContext.Users.ToListAsync();
            return AllUsers;
        }

        public async Task<User> GetUserByID(int userId)
        {
            var user = await _myContext.Users.FindAsync(userId);
            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _myContext.SaveChangesAsync();
        }
    }
}
