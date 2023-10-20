using api.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]    
    public class TrySomethingController : ControllerBase
    {
        private readonly ILogger<TrySomethingController> _logger;

        public TrySomethingController(ILogger<TrySomethingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Method([FromBody] Request request)
        {
            return await Task.FromResult(StatusCode(200, "Hello v1"));
        }
    }
}
