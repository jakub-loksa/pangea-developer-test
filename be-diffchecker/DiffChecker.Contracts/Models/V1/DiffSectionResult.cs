namespace DiffChecker.Contracts.Models.V1
{
    public class DiffSectionResult
    {
        public int Offset { get; set; }

        public int Length { get; set; }

        public int? LastIndex { get; set; }
    }
}