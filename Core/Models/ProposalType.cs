using System.Collections.Generic;

namespace ProjectFinal101.Core.Models
{
    public class ProposalType
    {
        public byte Id { get; set; }

        public string Description { get; set; }

        public IEnumerable<Proposal> Proposals { get; set; }

        public ProposalType()
        {
            Proposals = new List<Proposal>();
        }
    }
}
