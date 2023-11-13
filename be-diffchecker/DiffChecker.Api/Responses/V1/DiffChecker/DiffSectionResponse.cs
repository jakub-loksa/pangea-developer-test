namespace DiffChecker.Api.Responses.V1.DiffChecker
{
    /// <summary>
    /// Response model of an individual diff section.
    /// </summary>
    public class DiffSectionResponse
    {
        /// <summary>
        /// Zero-indexed offset at which the difference appears.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Length of differing characters.
        /// </summary>
        public int Length { get; set; }
    }
}
