using LeagueOP.Models;
using LeagueOP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeagueOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerController : ControllerBase
    {
        private readonly ILeagueApiCaller _caller;
        public SummonerController(ILeagueApiCaller caller)
        {
            _caller = caller;
        }

        [HttpGet()]
        public async Task<IActionResult> GetSummonerByRegionAndName([FromQuery] string name, CancellationToken cancellationToken)
        {
            var summoner = await _caller.CallEndpointAsync<SummonerDTO>($"summoner/v4/summoners/by-name/{name}", cancellationToken);
            return Ok(summoner);
        }
    }
}
