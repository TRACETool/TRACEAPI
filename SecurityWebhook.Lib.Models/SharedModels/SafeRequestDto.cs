using SecurityWebhook.Lib.Models.SafetyUtils;

namespace SecurityWebhook.Lib.Models.SharedModels
{
    public class SafeRequestDto<TParam>
    {
        public string Request {  get; set; }
        public string ERK { get; set; }

        public TParam DecryptRequestString(ISafetyUtility safetyUtility)
        {
            var request = safetyUtility.Decrypt<TParam>(Request, ERK);
            return request;
        }
    }

    
}
