using System.Collections.Generic;

namespace DiffChecker.Contracts.Models.V1
{
    public class DiffProcessingResult
    {
        public DiffProcessingResult(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public DiffProcessingResult(bool isSuccessful, string message, IEnumerable<DiffSectionResult> diffSections)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            DiffSections = diffSections;
        }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; } = null!;

        public IEnumerable<DiffSectionResult>? DiffSections { get; set; } = null;
    }
}
