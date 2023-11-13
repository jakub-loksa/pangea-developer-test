using Database.Contracts.Stores;
using DiffChecker.Contracts.Models.V1;
using DiffChecker.Contracts.Services.V1;
using DiffChecker.Logic.Algorithms;

namespace DiffChecker.Logic.Services.V1
{
    public class DiffService : IDiffService
    {
        private readonly IDiffStore _store;

        public DiffService(
            IDiffStore store)
        {
            _store = store;
        }

        public async Task<DiffProcessingResult> CalculateDiff(string id, CancellationToken cancellationToken)
        {
            var diff = await _store.TryGetDiff(id, cancellationToken);

            if (diff is null)
            {
                return new DiffProcessingResult(false, "Diff with the specified ID does not exist.");
            }

            if (string.IsNullOrEmpty(diff.LeftDiff))
            {
                return new DiffProcessingResult(false, "Diff with the specified ID is missing the left side.");
            }

            if (string.IsNullOrEmpty(diff.RightDiff))
            {
                return new DiffProcessingResult(false, "Diff with the specified ID is missing the right side.");
            }

            if (diff.LeftDiff == diff.RightDiff)
            {
                return new DiffProcessingResult(true, "inputs were equal");
            }

            if (diff.LeftDiff.Length != diff.RightDiff.Length)
            {
                return new DiffProcessingResult(true, "inputs are of different size");
            }

            return new DiffProcessingResult(true, "inputs are different", DiffAlgorithm.ProcessDiff(diff.LeftDiff, diff.RightDiff));
        }

        public async Task SaveLeftDiff(string id, string input, CancellationToken cancellationToken)
        {
            await _store.UpsertDiff(id,
                leftInput: input,
                rightInput: null,
                cancellationToken);
        }

        public async Task SaveRightDiff(string id, string input, CancellationToken cancellationToken)
        {
            await _store.UpsertDiff(id,
                leftInput: null,
                rightInput: input,
                cancellationToken);
        }
    }
}
