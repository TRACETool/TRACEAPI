using SecurityWebhook.Lib.Models.Enums;

namespace SecurityWebhook.Lib.Models.ContributorModels
{
    public class ContributorAuthResponse
    {
        public AuthEnums AuthEnums { get; set; }
        public string Message { get; set; }
    }
}
