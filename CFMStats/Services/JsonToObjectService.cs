namespace CFMStats.Services
{
    public class JsonToObjectService
    {
        public T ReturnJsonObject<T>(string url) where T : new()
        {
            var responseBody = UrlDataReaderService.GetDataFromUrl(url);

            // using Newtonsoft.Json;
            // return !string.IsNullOrEmpty(responseBody) ? JsonConvert.DeserializeObject<T>(responseBody) : new T();
            return !string.IsNullOrEmpty(responseBody) ? System.Text.Json.JsonSerializer.Deserialize<T>(responseBody) : new T();
        }
    }
}