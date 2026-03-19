using DiffChecker.Api.Responses.V1.DiffChecker;
using DiffChecker.Contracts.Models.V1;
using Riok.Mapperly.Abstractions;

namespace DiffChecker.Api.Mappers
{
    /// <summary>
    /// Sets up automatic mapping between result and response objects.
    /// </summary>
    [Mapper]
    public partial class ApiMapper
    {
        /// <summary>
        /// Maps a <see cref="DiffProcessingResult"/> to a <see cref="DiffProcessingResponse"/>.
        /// </summary>
        /// <param name="result">The diff processing result to map.</param>
        /// <returns>The mapped diff processing response.</returns>
        public partial DiffProcessingResponse ToResponse(DiffProcessingResult result);

        /// <summary>
        /// Maps a <see cref="DiffSectionResult"/> to a <see cref="DiffSectionResponse"/>.
        /// </summary>
        /// <param name="result">The diff section result to map.</param>
        /// <returns>The mapped diff section response.</returns>
        private partial DiffSectionResponse ToResponse(DiffSectionResult result);
    }
}
