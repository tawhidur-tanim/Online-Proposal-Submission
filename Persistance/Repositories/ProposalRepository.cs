using Microsoft.EntityFrameworkCore;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
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

        public IList<Proposal> GetProposals()
        {
            var activeSemester = Context.Semesters.FirstOrDefault(x => x.Status == StatusDetails.Active);

            var proposals = Entities
                .Include(x => x.Student)
                .Include(x => x.Student.Supervisor)
                .Include(x => x.Student.Reviewer)
                .Where(x => x.Student.SemesterId == activeSemester.Id)
                .ToList();

            return proposals;
        }

        public IList<Proposal> GetProposalCount(int activeSemesterId)
        {
            return Entities.Include(x => x.Student)
                .Where(x => x.Student.SemesterId == activeSemesterId).ToList();
        }

        public IList<Proposal> GetProposals(ParamResource resource)
        {

            var activeSemester = Context.Semesters.FirstOrDefault(x => x.Status == StatusDetails.Active);

            var proposals = Entities
                .Include(x => x.Student)
                .Include(x => x.Student.Supervisor)
                .Include(x => x.Student.Reviewer)
                .Where(x => x.Student.SemesterId == activeSemester.Id)
                .AsQueryable();

            foreach (var filter in resource.Filters)
            {
                switch (filter.Column)
                {
                    case "seminarAllow":
                        proposals = proposals.Where(x => x.Student.IsSeminar.Value == (bool)filter.Value);
                        break;
                    case "ProposalTypeId" when !string.IsNullOrEmpty(filter.Value.ToString()):
                        proposals = proposals.Where(x => x.ProposalTypeId == (int)filter.Value);
                        break;
                    case "Status" when !string.IsNullOrEmpty(filter.Value.ToString()):
                        proposals = proposals.Where(x => x.Status == (byte)filter.Value);
                        break;
                    default:
                        break;
                }
            }


            return proposals.ToList();
        }
    }
}
