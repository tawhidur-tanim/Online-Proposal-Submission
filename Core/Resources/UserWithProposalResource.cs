using System.Collections.Generic;

namespace ProjectFinal101.Core.Resources
{
    public class UserWithProposalResource
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public int? SemesterId { get; set; }

        public bool? IsSeminar { get; set; }

        public IEnumerable<ProposalWithOutNavResource> Proposals { get; set; }
    }
}
