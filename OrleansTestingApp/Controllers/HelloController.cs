using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansTestingApp.Interfaces;

namespace OrleansTestingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly IGrainFactory _client;

        public HelloController(IGrainFactory client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Something()
        {
            var friend = _client.GetGrain<IHelloGrain>("friend");
            var result = friend.SayHello("Hello");
            return Ok(result);
        }
    }
}
