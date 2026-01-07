using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDV.Application.Services.Interfaces;

namespace PDV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ConfigController : ControllerBase
    {
        private readonly IConfigService _service;

        public ConfigController(IConfigService service)
        {
            _service = service;
        }
    }
}
