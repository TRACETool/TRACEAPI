namespace SecurityWebhook.Lib.Models.SafetyUtils
{
    public interface ISafetyUtility
    {
        (string, string) Encrypt(string? response);
        T Decrypt<T>(string encryptedRequest, string encryptedRandomKey);
    }
}
