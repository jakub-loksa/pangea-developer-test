using Database.Contracts.Models.Diff;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Contracts.Stores
{
    public interface IDiffStore
    {
        Task<DiffDto?> TryGetDiff(string id, CancellationToken cancellationToken);

        Task UpsertDiff(string id, string? leftInput, string? rightInput, CancellationToken cancellationToken);
    }
}
