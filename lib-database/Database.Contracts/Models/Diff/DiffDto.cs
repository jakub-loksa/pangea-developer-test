namespace Database.Contracts.Models.Diff
{
    public class DiffDto
    {
        public string Id { get; set; } = null!;

        public string? LeftDiff { get; set; }

        public string? RightDiff { get; set; }
    }
}
