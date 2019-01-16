using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFinal101.Persistance.Repositories
{
    public class SemesterCatagoryRepository : BaseRepository<SemesterCatagory>, ISemesterCatagoryRepository
    {
        public SemesterCatagoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<SemesterCatagory> GetBySemesterId(int semeserId)
        {
            return Entities.Where(x => x.SemesterId == semeserId).ToList();
        }
    }
}
