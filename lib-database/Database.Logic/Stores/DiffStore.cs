using Database.Contracts.Models.Diff;
using Database.Contracts.Stores;
using Database.Logic.Entities.Diff;
using Database.Logic.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Database.Logic.Stores
{
    public sealed class DiffStore(
        DatabaseContext context,
        DiffMapper mapper) : IDiffStore
    {
        public async Task<DiffDto?> TryGetDiff(string id, CancellationToken cancellationToken)
        {
            var entity = await context.Diffs.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return mapper.ToDto(entity);
        }

        public async Task UpsertDiff(string id, string? leftInput, string? rightInput, CancellationToken cancellationToken)
        {
            var entity = await context.Diffs.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity is null)
            {
                entity = new DiffEntity
                {
                    Id = id
                };
                context.Diffs.Add(entity);
            }

            if (leftInput != null)
            {
                entity.LeftDiff = leftInput;
            }

            if (rightInput != null)
            {
                entity.RightDiff = rightInput;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
