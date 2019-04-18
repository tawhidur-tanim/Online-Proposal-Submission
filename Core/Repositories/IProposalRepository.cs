using ProjectFinal101.Core.Models;
using System.Collections.Generic;

namespace ProjectFinal101.Core.Repositories
{
    public interface IProposalRepository : IBaserepository<Proposal>
    {
        IList<Proposal> GetProposalByStudent(string userId);

        IList<Proposal> GetProposals();

        IList<Proposal> GetProposalCount(int activeSemesterId);

    }
}
