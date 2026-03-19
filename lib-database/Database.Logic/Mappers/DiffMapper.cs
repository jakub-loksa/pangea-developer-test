using Database.Contracts.Models.Diff;
using Database.Logic.Entities.Diff;
using Riok.Mapperly.Abstractions;

namespace Database.Logic.Mappers
{
    [Mapper]
    public partial class DiffMapper
    {
        public partial DiffDto? ToDto(DiffEntity? entity);
    }
}
