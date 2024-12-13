using Newtonsoft.Json;
using Plugin.DoubleEncryption;
using SecurityWebhook.Lib.Models.Constants;
using System.Text;
namespace SecurityWebhook.Lib.Models.SafetyUtils
{
    public class SafetyUtility : ISafetyUtility
    {
        private readonly AesBcCrypto _aesGcm;

        public SafetyUtility(AesBcCrypto aesGcm)
        {
            _aesGcm = aesGcm;
        }

        public (string,string) Encrypt(string? response)
        {
            var randomKey = Guid.NewGuid().ToString("N");
            var encryptedRandomKey = _aesGcm.Encrypt(randomKey, Encoding.UTF8.GetBytes(AuthConstants.EK));
            if (!string.IsNullOrEmpty(response))
            {
                var encryptedResponse = _aesGcm.Encrypt(response, Encoding.UTF8.GetBytes(randomKey));
                return(encryptedResponse,encryptedRandomKey);
            }
            return (null, encryptedRandomKey);

        }

        public T Decrypt<T>(string encryptedRequest, string encryptedRandomKey)
        {
            var randomKey = _aesGcm.Decrypt(encryptedRandomKey, Encoding.UTF8.GetBytes(AuthConstants.EK));
            if (!string.IsNullOrEmpty(encryptedRequest)) 
            {
                var decrypted = _aesGcm.Decrypt(encryptedRequest, Encoding.UTF8.GetBytes(randomKey));
                var request = JsonConvert.DeserializeObject<T>(decrypted);
                return request;
            }

            return default(T);

        }
    }
}
