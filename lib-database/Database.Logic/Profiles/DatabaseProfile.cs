using AutoMapper;
using Database.Contracts.Models.Diff;
using Database.Logic.Entities.Diff;

namespace Database.Logic.Profiles
{
    /// <summary>
    /// AutoMapper profile mapping DTOs to EF Entities.
    /// </summary>
    public class DatabaseProfile : Profile
    {
        public DatabaseProfile()
        {
            CreateDiffMappings();
        }

        private void CreateDiffMappings()
        {
            CreateMap<DiffDto, DiffEntity>()
                .ReverseMap();
        }
    }
}
