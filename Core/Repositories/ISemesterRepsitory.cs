using ProjectFinal101.Core.Models;
using System.Collections.Generic;

namespace ProjectFinal101.Core.Repositories
{
    public interface ISemesterRepsitory : IBaserepository<Semester>
    {
        IList<Semester> GetWithCategories();
    }
}
