using DiffChecker.Contracts.Models.V1;
using System.Threading;
using System.Threading.Tasks;

namespace DiffChecker.Contracts.Services.V1
{
    public interface IDiffService
    {
        Task SaveLeftDiff(string id, string input, CancellationToken cancellationToken);

        Task SaveRightDiff(string id, string input, CancellationToken cancellationToken);

        Task<DiffProcessingResult> CalculateDiff(string id, CancellationToken cancellationToken);
    }
}
