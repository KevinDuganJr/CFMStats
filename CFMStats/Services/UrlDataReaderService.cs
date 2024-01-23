using System.IO;
using System.Net;
using System.Net.Http;

namespace CFMStats.Services
{
    public class UrlDataReaderService
    {
        public static HttpClient httpClient = new HttpClient();

        public static string GetDataFromUrl(string url)
        {
            return HttpClient(url);
        }

        public static string WebClient(string url)
        {
            var webClient = new WebClient();
            var returnString = webClient.DownloadString(url);

            return returnString;
        }

        public static string HttpClient(string url)
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, url);
            //var response = httpClient.Send(request);
            //using var reader = new StreamReader(response.Content.ReadAsStream());
            //var responseBody = reader.ReadToEnd();

            var response = httpClient.GetStringAsync(url).Result;

            return response;
        }

    }
}