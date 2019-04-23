using ProjectFinal101.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectFinal101.Core.Repositories
{
    public interface IUserRepository : IBaserepository<ApplicationUser>
    {
        void RemoveBulk(List<ApplicationUser> users);

        Task InsertBulk(List<ApplicationUser> users);

        Task AssignRoles(List<ApplicationUser> users, string roleName);

        IList<ApplicationUser> UserSearch(string query, string role);

        IList<ApplicationUser> GetStudentsByTeacher(string teacherId, bool type);

        IList<MarksCatagory> GetCategoryByStudent(int semesterId, byte type);

        IList<StudentMarkMap> GetStudentMarks(string studentId, IEnumerable<int> categories);

        void SaveStudentMarks(List<StudentMarkMap> marksMap);

        void RemoveMarksMap(IEnumerable<string> marks);

        void PassStudent(string studentId);

        ApplicationUser GetStudent(string studentId);

        int GetStudentTotalMarks(string studentId);
    }
}
