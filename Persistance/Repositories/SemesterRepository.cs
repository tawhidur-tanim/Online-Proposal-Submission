using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFinal101.Persistance.Repositories
{
    public class SemesterRepository : BaseRepository<Semester>, ISemesterRepsitory
    {
        public SemesterRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public IList<Semester> GetWithCategories()
        {
            return Entities.
                 Include(x => x.SemesterCatagories)
                .ThenInclude(m => m.MarksCatagory).ToList();
        }

        public Semester GetWithCategory(int id)
        {
            return Entities.Include(x => x.SemesterCatagories)
                .ThenInclude(s => s.MarksCatagory)
                .FirstOrDefault(x => x.Id == id);
        }

        public void RemoveBulk(IList<ApplicationUser> students)
        {
            //Context.BulkDelete(students);

            var ids = students.Select(x => x.UserName);

            var removeList = Context.ApplicationUsers.Where(x => ids.Contains(x.UserName)).ToList();

            Context.BulkDelete(removeList);
        }

        public Semester GetCurrentSemester()
        {
            return Entities.FirstOrDefault(x => x.Status == StatusDetails.Active);
        }
    }
}
