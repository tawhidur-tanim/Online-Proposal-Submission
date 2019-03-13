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

        public IList<ApplicationUser> SupervisorSearch(string query)
        {
            var sups = Entities.Join(Context.UserRoles, x => x.Id, ur => ur.UserId, (user, role) => new
            {
                user,
                role
            }).Join(Context.Roles, x => x.role.RoleId, r => r.Id, (userRole, role) => new { userRole, role })
                .Where(x => (x.userRole.user.FullName.Contains(query) || x.userRole.user.UserName.Contains(query)) &&
                            x.role.Name == RoleReference.Student).Select(x => x.userRole.user).Take(10).ToList();

            return sups;
        }
    }
}
