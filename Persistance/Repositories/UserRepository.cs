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

        public IList<ApplicationUser> UserSearch(string query, string roleName)
        {
            var sups = Entities.Join(Context.UserRoles, x => x.Id, ur => ur.UserId, (user, role) => new
            {
                user,
                role
            }).Join(Context.Roles, x => x.role.RoleId, r => r.Id, (userRole, role) => new { userRole, role })
                .Where(x => (x.userRole.user.FullName.Contains(query) || x.userRole.user.UserName.Contains(query)) &&
                            x.role.Name == roleName).Select(x => x.userRole.user).Take(10).ToList();

            return sups;
        }


        public IList<ApplicationUser> GetStudentsByTeacher(string teacherId, bool type)
        {
            var students = Entities
                .Include(x => x.Proposals)
                .Where(x => !x.IsPassed);

            students = type
                ? students.Where(x => x.SupervisorId == teacherId)
                : students.Where(x => x.ReviewerId == teacherId);

            var users = students.ToList();

            foreach (var user in users)
            {
                user.Proposals = user.Proposals.Where(y => y.Status == ProposalStstus.Accepted);
            }

            return users;
        }

        public IList<MarksCatagory> GetCategoryByStudent(int semesterId, byte type)
        {
            var categories = Context
                .SemesterCatagories
                .Include(x => x.MarksCatagory)
                .Where(x => x.SemesterId == semesterId)
                .Select(x => x.MarksCatagory).ToList().Where(x => x.MarkType == type).ToList();

            return categories;
        }

        public IList<StudentMarkMap> GetStudentMarks(string studentId, IEnumerable<int> categories)
        {
            var maps = Context
                .StudentMarkMaps
                .Where(x => categories.Contains(x.MarksId) && x.StudentId == studentId)
                .ToList();

            return maps;
        }

        public void SaveStudentMarks(List<StudentMarkMap> marksMap)
        {
            Context.StudentMarkMaps.AddRange(marksMap);
        }

        public void RemoveMarksMap(IEnumerable<string> marks)
        {
            var maps = Context.StudentMarkMaps.Where(x => marks.Contains(x.StudentId));

            Context.StudentMarkMaps.RemoveRange(maps);
        }

        public void PassStudent(string studentId)
        {
            var student = Entities.FirstOrDefault(x => x.Id == studentId);

            if (student != null) student.IsPassed = true;
        }

        public ApplicationUser GetStudent(string studentId)
        {
            return Entities
                 .Include(x => x.Reviewer)
                 .Include(x => x.Supervisor)
                 .FirstOrDefault(x => x.Id == studentId);
        }
    }
}
