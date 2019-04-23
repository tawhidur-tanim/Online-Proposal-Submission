using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Resources;
using System.Collections.Generic;

namespace ProjectFinal101.Core.Repositories
{
    public interface IProposalRepository : IBaserepository<Proposal>
    {
        IList<Proposal> GetProposalByStudent(string userId);

        IList<Proposal> GetProposals();

        IList<Proposal> GetProposalCount(int activeSemesterId);

        IList<Proposal> GetProposals(ParamResource resource);
    }
}
