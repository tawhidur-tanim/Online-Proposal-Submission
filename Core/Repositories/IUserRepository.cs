using ProjectFinal101.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectFinal101.Core.Repositories
{
    public interface IUserRepository : IBaserepository<ApplicationUser>
    {
        void RemoveBulk(List<ApplicationUser> users);

        Task InsertBulk(List<ApplicationUser> users);

        Task AssignRoles(List<ApplicationUser> users);

        IList<ApplicationUser> UserSearch(string query, string role);

        IList<ApplicationUser> GetStudentsByTeacher(string teacherId, bool type);
    }
}
