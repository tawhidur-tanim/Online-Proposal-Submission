using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;

namespace ProjectFinal101.Persistance.Repositories
{
    public class SemesterRepository : BaseRepository<Semester>, ISemesterRepsitory
    {
        public SemesterRepository(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}
