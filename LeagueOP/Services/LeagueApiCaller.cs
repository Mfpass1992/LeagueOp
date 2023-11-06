using LeagueOP.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueOP.Services
{
    public class LeagueApiCaller : ILeagueApiCaller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LeagueApiCaller> _logger;

        public LeagueApiCaller(IHttpClientFactory httpClientFactory,
                               IOptions<LeagueApiOptions> options,
                               ILogger<LeagueApiCaller> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-9fa0baaf-ba64-4050-9dba-27fe42a615d6");
            _logger = logger;
        }

        public async Task<TResponse> CallEndpointAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint, cancellationToken);
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonSerializer.Deserialize<TResponse>(responseContent);

                return result;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error calling external API.");
                throw;
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error deserializing response.");
                throw;
            }
        }
    }

    public class LeagueApiOptions
    {
        public string BaseUrl { get; set; }
    }
}
