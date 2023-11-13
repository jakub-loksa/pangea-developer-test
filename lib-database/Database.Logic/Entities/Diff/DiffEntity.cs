using Database.Contracts.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Logic.Entities.Diff
{
    /// <summary>
    /// Entity for storing diffs for future calculations.
    /// </summary>
    public class DiffEntity
    {
        /// <summary>
        /// User-defined ID of the diff.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(DataConstraints.DefaultStringLength)]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Left side to compare the right side with.
        /// </summary>
        [StringLength(DataConstraints.DefaultStringLength)]
        public string? LeftDiff { get; set; }

        /// <summary>
        /// Right side to compare the left side with.
        /// </summary>
        [StringLength(DataConstraints.DefaultStringLength)]
        public string? RightDiff { get; set; }
    }
}
