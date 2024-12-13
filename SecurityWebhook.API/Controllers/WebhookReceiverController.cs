using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecurityWebhook.API.Constants.Paths;
using SecurityWebhook.Lib.Models.GithubModels;
using SecurityWebhoook.Lib.Services.WebhookServices;

namespace SecurityWebhook.API.Controllers
{
    
    [ApiController]
    public class WebhookReceiverController : ControllerBase
    {
        private readonly IWebhookService _webhookService;

        public WebhookReceiverController(IWebhookService webhookService) 
        {       
            _webhookService = webhookService;        
        }

        [HttpPost(WebhookReceiverPath.GetGithubWebhook)]
        public async Task<IActionResult> GetGithubWebhookAsync(object githubData)
        {
            //if (!Request.Headers.TryGetValue("X-GitHub-Event", out var actionHeader))
            //    return BadRequest("Missing 'X-GitHub-Event' header.");

            var action = "push";//actionHeader.ToString();

            await _webhookService.GetGithubWebhookAsync(githubData, action);

            return Ok(true);
        }
    }
}
