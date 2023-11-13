using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiffChecker.Api.Tests.Helpers
{
    internal static class JsonHelper
    {
        internal static readonly JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static Task<string> DeserializeStringContent(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync();
        }

        public static async Task<T> DeserializeJsonContent<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(responseContent, Options)!;
        }
    }
}
