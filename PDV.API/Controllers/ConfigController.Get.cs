using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PDV.API.Controllers
{
    public partial class ConfigController 
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _service.GetConfigAsync());
        }
    }
}
