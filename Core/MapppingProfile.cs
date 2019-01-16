using AutoMapper;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Resources;
using System.Linq;

namespace ProjectFinal101.Core
{
    public class MapppingProfile : Profile
    {
        public MapppingProfile()
        {
            // resource to domain
            CreateMap<SemesterCreateResource, Semester>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.SemesterCatagories, opt => opt.MapFrom(sr =>
                        sr.Catagories
                            .Select(c =>
                                new SemesterCatagory
                                {
                                    MarksCatagory = new MarksCatagory
                                    {
                                        Name = c.Name,
                                        Mark = c.Mark
                                    }
                                }
                            )
                    )
                );


            CreateMap<MarksCatagoryResource, MarksCatagory>();


            // domain to resource
            CreateMap<Semester, SemesterCreateResource>()
                .ForMember(x => x.Catagories, op => op.Ignore())
                .AfterMap(((semester, resource) =>
                {

                    foreach (var catagory in semester.SemesterCatagories)
                    {
                        var marksCatagoryResource = new MarksCatagoryResource { Id = catagory.MarksCatagoryId };

                        if (catagory.MarksCatagory != null)
                        {
                            marksCatagoryResource.Name = catagory.MarksCatagory.Name;
                            marksCatagoryResource.Mark = catagory.MarksCatagory.Mark;
                        }

                        resource.Catagories.Add(marksCatagoryResource);
                    }

                }));

        }
    }
}
