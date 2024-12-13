using SecurityWebhook.Lib.Models.Enums;

namespace SecurityWebhook.Lib.Models.ContributorModels
{
    public class UserLoginInfo
    {
        public AuthEnums Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
