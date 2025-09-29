using ArsenalTechnicalAssignment.Data.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;

namespace ArsenalTechnicalAssignment.Data.Services
{
    public class ExternalIntegrationService
    {
        private HttpClient _httpClient = new HttpClient();
        private string _serviceURL;

        public ExternalIntegrationService(string serviceURL)
        {
            _serviceURL = serviceURL;
        }

        public JsonSerializerOptions _serializerOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };


        public async Task<string> ExecuteGET(string url)
        {
            string result = "Exception";
            Uri uri = new Uri(string.Format($"{_serviceURL}{url}", string.Empty));
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
                {
                    // Add API Key to the request header
                    request.Headers.Add("X-Auth-Token", 
                        $"{ExternalAPIConstants.FootballDataAPIKey}");

                    HttpResponseMessage response = await _httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }

            return result;
        }
    }
}
