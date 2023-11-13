using DiffChecker.Logic.Algorithms;
using FluentAssertions;

namespace DiffChecker.Api.Tests.Logic.Algorithms
{
    public class DiffAlgorithmTests
    {
        [Theory]
        [InlineData("A", "B", 0, 1)]
        [InlineData("AA", "AB", 1, 1)]
        [InlineData("AAAAA", "ABBBA", 1, 3)]
        public void DiffAlgorithmTests_OneDiff(string left, string right, int offset, int length)
        {
            var diffs = DiffAlgorithm.ProcessDiff(left, right);
            diffs.Should().HaveCount(1);

            var diff = diffs.Single();
            diff.Offset.Should().Be(offset);
            diff.Length.Should().Be(length);
        }

        [Fact]
        public void DiffAlgorithmTests_MultipleDiffs()
        {
            var left = "AAAAAA123AA";
            var right = "AABBBA789AA";

            var diffs = DiffAlgorithm.ProcessDiff(left, right);
            diffs.Should().HaveCount(2);

            var firstDiff = diffs.First();
            firstDiff.Offset.Should().Be(2);
            firstDiff.Length.Should().Be(3);

            var secondDiff = diffs.Last();
            secondDiff.Offset.Should().Be(6);
            secondDiff.Length.Should().Be(3);
        }
    }
}
