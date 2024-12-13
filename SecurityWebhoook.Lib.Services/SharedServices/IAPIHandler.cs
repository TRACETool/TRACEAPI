namespace SecurityWebhoook.Lib.Services.SharedServices
{
    public interface IAPIHandler
    {
        Task<TReturn> GetAsync<TReturn>(string url, string baseUrl, Dictionary<string, string> headers = null);
        Task<TReturn> PostAsync<TReturn>(string json, string url, string baseUrl, Dictionary<string, string> headers = null);
        Task<TReturn> PostAsync<TReturn, TParam>(TParam json, string url, string baseUrl, Dictionary<string, string> headers = null);



    }
}
