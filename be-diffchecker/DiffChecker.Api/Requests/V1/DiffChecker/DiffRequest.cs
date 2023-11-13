using Database.Contracts.Constants;
using System.ComponentModel.DataAnnotations;

namespace DiffChecker.Api.Requests.V1.DiffChecker
{
    /// <summary>
    /// Request for saving diff data.
    /// </summary>
    public class DiffRequest
    {
        /// <summary>
        /// Text to be saved for future diff requests.
        /// </summary>
        [Required]
        [StringLength(DataConstraints.DefaultStringLength)]
        public string Input { get; set; } = null!;
    }
}
