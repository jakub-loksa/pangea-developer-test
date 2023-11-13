using AutoMapper;
using Database.Contracts.Models.Diff;
using Database.Contracts.Stores;
using Database.Logic.Entities.Diff;
using Microsoft.EntityFrameworkCore;

namespace Database.Logic.Stores
{
    public sealed class DiffStore : IDiffStore
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public DiffStore(
            DatabaseContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DiffDto?> TryGetDiff(string id, CancellationToken cancellationToken)
        {
            var entity = await _context.Diffs.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return _mapper.Map<DiffDto>(entity);
        }

        public async Task UpsertDiff(string id, string? leftInput, string? rightInput, CancellationToken cancellationToken)
        {
            var entity = await _context.Diffs.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity is null)
            {
                entity = new DiffEntity
                {
                    Id = id
                };
                _context.Diffs.Add(entity);
            }

            if (leftInput != null)
            {
                entity.LeftDiff = leftInput;
            }

            if (rightInput != null)
            {
                entity.RightDiff = rightInput;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
