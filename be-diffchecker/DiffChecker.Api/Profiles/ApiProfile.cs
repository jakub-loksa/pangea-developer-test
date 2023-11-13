using AutoMapper;
using DiffChecker.Api.Responses.V1.DiffChecker;
using DiffChecker.Contracts.Models.V1;

namespace DiffChecker.Api.Profiles
{
    /// <summary>
    /// AutoMapper profile mapping service results to responses.
    /// </summary>
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateDiffMappings();
        }

        private void CreateDiffMappings()
        {
            CreateMap<DiffSectionResult, DiffSectionResponse>();
            CreateMap<DiffProcessingResult, DiffProcessingResponse>();
        }
    }

}
