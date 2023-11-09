using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stories.Api.Service
{
    public abstract class BaseClient
    {
        private static readonly HttpMethod PatchHttpMethod = new HttpMethod("PATCH");

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = { (JsonConverter)new StringEnumConverter() },
            NullValueHandling = NullValueHandling.Ignore
        };

        private readonly HttpClient _httpClient;

        private readonly JsonSerializer _jsonSerializer;

        //
        // Summary:
        //     Initializes a new instance of the BaseClient class.
        //
        // Parameters:
        //   httpClient:
        //     Http client.
        protected BaseClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _jsonSerializer = JsonSerializer.Create(JsonSerializerSettings);
        }

        //
        // Summary:
        //     Disposes base client.
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
         

        //
        // Summary:
        //     Http GET.
        //
        // Parameters:
        //   requestUri:
        //     Request uri.
        //
        //   cancellationToken:
        //     Cancellation token.
        //
        // Type parameters:
        //   T:
        //     Response type.
        //
        // Returns:
        //     Response.
        //
        // Exceptions:
        //     Thrown if status isn't successful.
        protected async Task<T> GetAsync<T>(string requestUri, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await SendRequestAsync<T>(HttpMethod.Get, requestUri, null, cancellationToken);
        }
 

        private async Task<T> SendRequestAsync<T>(HttpMethod method, string requestUri, object content = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            using HttpRequestMessage request = new HttpRequestMessage(method, requestUri);
            if (content != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(content, JsonSerializerSettings), Encoding.UTF8, "application/json");
            }

            using HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(); 
            }

            using Stream stream = await response.Content.ReadAsStreamAsync();
            using StreamReader reader = new StreamReader(stream);
            using JsonTextReader reader2 = new JsonTextReader(reader);
            return _jsonSerializer.Deserialize<T>(reader2);
        } 

    }

}
