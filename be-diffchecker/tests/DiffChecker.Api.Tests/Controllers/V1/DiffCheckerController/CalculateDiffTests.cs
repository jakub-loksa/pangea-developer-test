using DiffChecker.Api.Responses.V1.DiffChecker;
using DiffChecker.Api.Tests.Helpers;
using FluentAssertions;
using System.Net;

namespace DiffChecker.Api.Tests.Controllers.V1.DiffCheckerController
{
    [Collection("IntegrationTests")]
    public class CalculateDiffTests : ApiTestBase
    {
        public CalculateDiffTests(ApiTestApplicationFactory factory)
            : base(factory)
        {
            _factory.SetUpTest();
            SetupDiffs();
        }

        [Fact]
        public async Task WhenDiffIsMissing_ShouldReturnDiffMissing()
        {
            _client = _factory.CreateUnauthorizedClient();

            var route = string.Format(RouteConstants.DiffRouteV1, "NOT_FOUND_DIFF");
            var response = await _client.Get<DiffProcessingResponse>(route, HttpStatusCode.OK);

            response.IsSuccessful.Should().BeFalse();
            response.Message.Should().Be("Diff with the specified ID does not exist.");
        }

        [Fact]
        public async Task WhenLeftDiffIsMissing_ShouldReturnLeftDiffMissing()
        {
            _client = _factory.CreateUnauthorizedClient();

            var route = string.Format(RouteConstants.DiffRouteV1, "LEFT_MISSING1");
            var response = await _client.Get<DiffProcessingResponse>(route, HttpStatusCode.OK);

            response.IsSuccessful.Should().BeFalse();
            response.Message.Should().Be("Diff with the specified ID is missing the left side.");
        }

        [Fact]
        public async Task WhenRightDiffIsMissing_ShouldReturnRightDiffMissing()
        {
            _client = _factory.CreateUnauthorizedClient();

            var route = string.Format(RouteConstants.DiffRouteV1, "RIGHT_MISSING1");
            var response = await _client.Get<DiffProcessingResponse>(route, HttpStatusCode.OK);

            response.IsSuccessful.Should().BeFalse();
            response.Message.Should().Be("Diff with the specified ID is missing the right side.");
        }

        [Fact]
        public async Task WhenDiffsAreEqual_ShouldReturnSuccessMessage()
        {
            _client = _factory.CreateUnauthorizedClient();

            var route = string.Format(RouteConstants.DiffRouteV1, "SAME1");
            var response = await _client.Get<DiffProcessingResponse>(route, HttpStatusCode.OK);

            response.IsSuccessful.Should().BeTrue();
            response.Message.Should().Be("inputs were equal");
        }

        [Fact]
        public async Task WhenDiffsAreOfDifferentSize_ShouldReturnSuccessMessage()
        {
            _client = _factory.CreateUnauthorizedClient();

            var route = string.Format(RouteConstants.DiffRouteV1, "SHORT1");
            var response = await _client.Get<DiffProcessingResponse>(route, HttpStatusCode.OK);

            response.IsSuccessful.Should().BeTrue();
            response.Message.Should().Be("inputs are of different size");
        }

        [Fact]
        public async Task WhenDiffsAreOfSameSize_ShouldReturnDiffResponse()
        {
            _client = _factory.CreateUnauthorizedClient();

            var route = string.Format(RouteConstants.DiffRouteV1, "SAMESIZE1");
            var response = await _client.Get<DiffProcessingResponse>(route, HttpStatusCode.OK);

            response.IsSuccessful.Should().BeTrue();
            response.Message.Should().Be("inputs are different");
            response.DiffSections.Should().HaveCount(1);
            var diff = response.DiffSections!.Single();
            diff.Offset.Should().Be(3);
            diff.Length.Should().Be(3);
        }
    }
}
