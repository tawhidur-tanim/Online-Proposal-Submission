using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;

namespace ProjectFinal101.Persistance.Repositories
{
    public class ProposalRepository : BaseRepository<Proposal>, IProposalRepository
    {
        public ProposalRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
