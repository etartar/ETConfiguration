using ETConfiguration.Core.Database.Entities;
using ETConfiguration.Core.Database.Repositories;
using ETConfiguration.Database.Sample.Configs;
using ETConfiguration.Database.Sample.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ETConfiguration.Database.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IConfigurationReadRepository _configurationReadRepository;
        private readonly IConfigurationWriteRepository _configurationWriteRepository;
        private readonly IOptionsSnapshot<ConfigSection> _config;

        public ConfigurationsController(IConfigurationReadRepository configurationReadRepository, IConfigurationWriteRepository configurationWriteRepository, IOptionsSnapshot<ConfigSection> config)
        {
            _configurationReadRepository = configurationReadRepository;
            _configurationWriteRepository = configurationWriteRepository;
            _config = config;
        }

        [HttpGet("ReadConfigs")]
        public IActionResult ReadConfigs()
        {
            return Ok(_config.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetConfigs()
        {
            return Ok(await _configurationReadRepository.GetListAsync(x => x.IsActive));
        }

        [HttpPost]
        public async Task<IActionResult> SaveConfig(CreateConfigurationDto createConfiguration)
        {
            var saveConfig = Configuration.Create(createConfiguration.Section, createConfiguration.Key, createConfiguration.Value);
            var savedConfig = await _configurationWriteRepository.AddAsync(saveConfig);
            return Ok(savedConfig);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConfig(UpdateConfigurationDto updateConfiguration)
        {
            var dbConfig = await _configurationReadRepository.GetAsync(x => x.Id == updateConfiguration.Id);
            var updateConfig = dbConfig.Update(updateConfiguration.Section, updateConfiguration.Key, updateConfiguration.Value);
            var updatedConfig = _configurationWriteRepository.Update(updateConfig);
            return Ok(updatedConfig);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig(Guid id)
        {
            var dbConfig = await _configurationReadRepository.GetAsync(x => x.Id == id);
            _configurationWriteRepository.Delete(dbConfig);
            return NoContent();
        }
    }
}
