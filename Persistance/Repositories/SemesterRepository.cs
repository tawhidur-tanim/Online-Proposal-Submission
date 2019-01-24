using Microsoft.EntityFrameworkCore;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
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
    }
}
