using ETConfiguration.Consul.Sample.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ETConfiguration.Consul.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsulController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptionsSnapshot<ConsulConfig> _config;

        public ConsulController(IConfiguration configuration, IOptionsSnapshot<ConsulConfig> config)
        {
            _configuration = configuration;
            _config = config;
        }

        [HttpGet]
        [Route("getValues")]
        public IActionResult GetValues()
        {
            return Ok(_config.Value);
        }
    }
}
