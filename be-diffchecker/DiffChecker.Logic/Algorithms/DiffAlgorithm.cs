using DiffChecker.Contracts.Models.V1;

namespace DiffChecker.Logic.Algorithms
{
    public class DiffAlgorithm
    {
        /// <summary>
        /// Algorithm for calculating diffs between two strings. We assume both strings are the same length.
        /// </summary>
        /// <param name="left">Left side of the comparision.</param>
        /// <param name="right">Right side of the comparision.</param>
        /// <returns>List of diffs.</returns>
        public static IEnumerable<DiffSectionResult> ProcessDiff(string left, string right)
        {
            var list = new List<DiffSectionResult>();

            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] == right[i])
                {
                    continue;
                }

                var latestDiff = list.LastOrDefault();

                if (latestDiff is null || latestDiff.LastIndex < i - 1)
                {
                    // In case this is a first difference after characters that were the same, we want to create a new diff section.
                    list.Add(new DiffSectionResult
                    {
                        Offset = i,
                        Length = 1,
                        LastIndex = i
                    });
                }
                else
                {
                    // In case of a continuous chain of different characters, we want to increase the length only.
                    latestDiff.LastIndex++;
                    latestDiff.Length++;
                }
            }

            return list;
        }
    }
}
