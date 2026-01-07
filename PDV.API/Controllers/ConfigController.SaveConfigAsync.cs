using Microsoft.AspNetCore.Mvc;
using PDV.Core.Entities;

namespace PDV.API.Controllers
{
    public partial class ConfigController
    {
        [HttpPost]
        public async Task<ActionResult> Save([FromBody] Config data)
        {
            try
            {
                await _service.SaveConfigAsync(data);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
