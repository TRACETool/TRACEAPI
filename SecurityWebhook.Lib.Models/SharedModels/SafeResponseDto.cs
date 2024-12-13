using SecurityWebhook.Lib.Models.SafetyUtils;

namespace SecurityWebhook.Lib.Models.SharedModels
{
    public class SafeResponseDto
    {
        public string Response { get; set; }
        public string ERK { get; set; }

        public void Encrypt(ISafetyUtility safetyUtility)
        {
            (Response, ERK) = safetyUtility.Encrypt(Response);
        }
    }
}
