namespace DiffChecker.Api.Responses.V1.DiffChecker
{
    /// <summary>
    /// Response model for Diff processing.
    /// </summary>
    public class DiffProcessingResponse
    {
        /// <summary>
        /// If false, the inputs are incorrect and must be updated.
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Message to describe the state of the diff.
        /// </summary>
        public string Message { get; set; } = null!;

        /// <summary>
        /// List of individual diff sections between left and right side.
        /// </summary>
        public IEnumerable<DiffSectionResponse>? DiffSections { get; set; }
    }
}
