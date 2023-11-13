using DiffChecker.Api.Tests.Helpers;
using FluentAssertions;
using System.Net;

namespace DiffChecker.Api.Tests
{
    public sealed class TestHttpClient
    {
        private readonly HttpClient _httpClient;

        public TestHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<T> Get<T>(string requestUri, HttpStatusCode expectedStatusCode)
        {
            var response = await _httpClient.GetAsync(requestUri);

            return await ProcessResponse<T>(response, expectedStatusCode);
        }

        public async Task<T> PostRaw<T>(string requestUri, string data, HttpStatusCode expectedStatusCode)
        {
            var response = await _httpClient.PostAsync(requestUri, new StringContent(data));

            return await ProcessResponse<T>(response, expectedStatusCode);
        }

        private async Task ProcessResponse(HttpResponseMessage response, HttpStatusCode expectedStatusCode)
        {
            response.StatusCode.Should().Be(expectedStatusCode, await response.Content.ReadAsStringAsync());
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response, HttpStatusCode expectedStatusCode)
        {
            await ProcessResponse(response, expectedStatusCode);

            if (typeof(T) == typeof(string))
            {
                return (T)(object)await JsonHelper.DeserializeStringContent(response);
            }

            return await JsonHelper.DeserializeJsonContent<T>(response);
        }
    }
}
