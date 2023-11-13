using DiffChecker.Api.Requests.V1.DiffChecker;
using DiffChecker.Api.Tests.Helpers;
using FluentAssertions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DiffChecker.Api.Tests.Controllers.V1.DiffCheckerController
{
    [Collection("IntegrationTests")]
    public class SaveLeftDiffTests : ApiTestBase
    {
        public SaveLeftDiffTests(ApiTestApplicationFactory factory)
            : base(factory)
        {
            _factory.SetUpTest();
        }

        [Fact]
        public async Task WhenInvalidBase64String_ShouldReturn400BadRequest()
        {
            _client = _factory.CreateUnauthorizedClient();

            var route = string.Format(RouteConstants.DiffLeftRouteV1, "NOT_FOUND_DIFF");
            var errorResponse = await _client.PostRaw<string>(route, "INVALID_BASE64_REQUEST", HttpStatusCode.BadRequest);

            errorResponse.Should().Be("\"The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.\"");
        }

        [Fact]
        public async Task WhenRequestIsNotInCorrectFormat_ShouldReturn400BadRequest()
        {
            _client = _factory.CreateUnauthorizedClient();

            var request = JsonSerializer.Serialize(new { InvalidProperty = "Invalid Value" });
            var requestBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(request));

            var route = string.Format(RouteConstants.DiffLeftRouteV1, "NOT_FOUND_DIFF");
            var errorResponse = await _client.PostRaw<string>(route, requestBase64, HttpStatusCode.BadRequest);

            errorResponse.Should().Be("\"The input is not in the correct format - it must be a Base-64 encoded JSON object, similar to {'input': 'value'}.\"");
        }

        [Fact]
        public async Task WhenDiffDoesNotExist_ShouldCreateNewDiff()
        {
            _client = _factory.CreateUnauthorizedClient();

            var request = JsonSerializer.Serialize(new DiffRequest { Input = "Test Value" }, JsonHelper.Options);
            var requestBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(request));

            var route = string.Format(RouteConstants.DiffLeftRouteV1, "NOT_FOUND_DIFF");
            await _client.PostRaw<string>(route, requestBase64, HttpStatusCode.NoContent);

            using var context = GetContext();
            context.Diffs.Should().HaveCount(1);
            var diff = context.Diffs.Single();
            diff.Id.Should().Be("NOT_FOUND_DIFF");
            diff.LeftDiff.Should().Be("Test Value");
            diff.RightDiff.Should().BeNull();
        }

        [Fact]
        public async Task WhenDiffExists_ShouldUpdateExistingDiff()
        {
            SetupDiffs();

            _client = _factory.CreateUnauthorizedClient();

            var request = JsonSerializer.Serialize(new DiffRequest { Input = "CHANGED_VALUE" }, JsonHelper.Options);
            var requestBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(request));

            var route = string.Format(RouteConstants.DiffLeftRouteV1, "SAME1");
            await _client.PostRaw<string>(route, requestBase64, HttpStatusCode.NoContent);

            using var context = GetContext();
            context.Diffs.Should().HaveCount(5, "It's the original count in the database.");
            var diff = context.Diffs.Single(x => x.Id == "SAME1");
            diff.LeftDiff.Should().Be("CHANGED_VALUE");
            diff.RightDiff.Should().Be("ABC123", "It's the original value stored in the database");
        }
    }
}
