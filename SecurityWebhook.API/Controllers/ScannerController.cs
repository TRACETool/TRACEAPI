using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityWebhook.API.Constants.Paths;

namespace SecurityWebhook.API.Controllers
{
    
    [ApiController]
    public class ScannerController : ControllerBase
    {
        public ScannerController() { }

        [HttpPost(ScannerPath.SemgrepReceiver)]
        public async Task<IActionResult> GetSemgrepResultAsync(string semgrepData)
        {
            return Ok(true);
        }
    }
}
