using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFinal101.Persistance.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
            : base(context)
        {
            _userManager = userManager;
        }


        public void RemoveBulk(List<ApplicationUser> users)
        {
            var userNames = users.Select(x => x.UserName);

            var usersInDb = Context.ApplicationUsers.Where(x => userNames.Contains(x.UserName)).ToList();

            Context.BulkDelete(usersInDb);
        }

        public async Task InsertBulk(List<ApplicationUser> users)
        {
            foreach (var user in users)
            {
                await _userManager.CreateAsync(user, user.UserName);
            }
        }

        public async Task AssignRoles(List<ApplicationUser> users)
        {
            foreach (var user in users)
            {
                await _userManager.AddToRoleAsync(user, RoleReference.Student);
            }
        }
    }
}
