using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFinal101.Persistance.Repositories
{
    public class ProposalRepository : BaseRepository<Proposal>, IProposalRepository
    {
        public ProposalRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IList<Proposal> GetProposalByStudent(string userId)
        {
            return Entities.Where(x => x.StudentId == userId).ToList();
        }
    }
}
