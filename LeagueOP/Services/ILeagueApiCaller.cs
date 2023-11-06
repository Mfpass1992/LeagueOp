using LeagueOP.Models;

namespace LeagueOP.Services
{
    public interface ILeagueApiCaller
    {
        public Task<TResponse> CallEndpointAsync<TResponse>(string endpoint, CancellationToken cancellationToken = default);
    }
}
