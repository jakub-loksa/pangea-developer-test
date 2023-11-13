using Asp.Versioning;
using AutoMapper;
using DiffChecker.Api.Requests.V1.DiffChecker;
using DiffChecker.Api.Responses.V1.DiffChecker;
using DiffChecker.Contracts.Services.V1;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiffChecker.Api.Controllers.V1
{
    /// <summary>
    /// API Controller for saving and processing diff checks.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v1/diff")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class DiffCheckerController : ControllerBase
    {
        private readonly IDiffService _service;
        private readonly IMapper _mapper;

        public DiffCheckerController(
            IDiffService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Saves left side of the diff.
        /// </summary>
        /// <param name="id">ID of the operation. Used later on for comparision.</param>
        [HttpPost("{id}/left")]
        [Consumes(MediaTypeNames.Text.Plain)]
        public async Task<IActionResult> SaveLeftDiff(
            [FromRoute(Name = "id")] string id,
            CancellationToken cancellationToken)
        {
            return await ProcessRequest(input => _service.SaveLeftDiff(id, input, cancellationToken));
        }

        /// <summary>
        /// Saves right side of the diff.
        /// </summary>
        /// <param name="id">ID of the operation. Used later on for comparision.</param>
        [HttpPost("{id}/right")]
        [Consumes(MediaTypeNames.Text.Plain)]
        public async Task<IActionResult> SaveRightDiff(
            [FromRoute(Name = "id")] string id,
            CancellationToken cancellationToken)
        {
            return await ProcessRequest(input => _service.SaveRightDiff(id, input, cancellationToken));
        }

        /// <summary>
        /// Calculates the difference between saved left and right sides of a diff.
        /// </summary>
        /// <param name="id">ID of the operation used by the left and right endpoints.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> CalculateDiff(
            [FromRoute(Name = "id")] string id,
            CancellationToken cancellationToken)
        {
            var result = await _service.CalculateDiff(id, cancellationToken);

            var response = _mapper.Map<DiffProcessingResponse>(result);

            return Ok(response);
        }

        /// <summary>
        /// Validates and converts user input and calls a method to process the parsed value.
        /// </summary>
        /// <param name="callback">Method to be called after successfully parsing user data.</param>
        private async Task<IActionResult> ProcessRequest(Func<string, Task> callback)
        {
            var reader = new StreamReader(Request.Body);
            var text = await reader.ReadToEndAsync();

            // When a user sends a JSON string, we want to strip the encompassing quotes so it is a valid Base64 string.
            var filteredTextContent = text.Replace("\"", string.Empty);

            byte[] convertedJsonBytes;
            try
            {
                convertedJsonBytes = Convert.FromBase64String(filteredTextContent);
            }
            catch (FormatException)
            {
                return BadRequest("The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.");
            }

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var request = JsonSerializer.Deserialize<DiffRequest>(convertedJsonBytes, jsonSerializerOptions);
            if (request is null || string.IsNullOrEmpty(request.Input))
            {
                return BadRequest("The input is not in the correct format - it must be a Base-64 encoded JSON object, similar to {'input': 'value'}.");
            }

            await callback(request.Input);

            return NoContent();
        }
    }
}
