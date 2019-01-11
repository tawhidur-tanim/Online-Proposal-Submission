using AutoMapper;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Resources;

namespace ProjectFinal101.Core
{
    public class MapppingProfile : Profile
    {
        public MapppingProfile()
        {
            // resource to domain
            CreateMap<SemesterCreateResource, Semester>()
                .ForMember(x => x.Id, opt => opt.Ignore());


            // domain to resource
            CreateMap<Semester, SemesterCreateResource>();
        }
    }
}
