using ProjectFinal101.Core.Models;
using System.Collections.Generic;

namespace ProjectFinal101.Core.Repositories
{
    public interface ISemesterCatagoryRepository : IBaserepository<SemesterCatagory>
    {
        List<SemesterCatagory> GetBySemesterId(int semeserId);
    }
}
