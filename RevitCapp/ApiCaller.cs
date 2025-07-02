using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RevitCapp;
using System.Data;
using DTOs;
using DTOs.Enum;

namespace RevitCapp
{
    public class ApiCaller
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://backendbpo-atcjd6ahftgyejcu.canadacentral-01.azurewebsites.net"),
            Timeout = TimeSpan.FromSeconds(30)
        };

        //public async Task<List<WeatherForecast>> GetWeatherForecastAsync()
        //{
        //    var token = await AzureAuthHelper.Instance.GetAccessTokenAsync();

        //    using (var request = new HttpRequestMessage(HttpMethod.Get, "/weatherforecast"))
        //    {
        //        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        using (var response = await _httpClient.SendAsync(request))
        //        {
        //            if (!response.IsSuccessStatusCode)
        //            {
        //                throw new HttpRequestException($"Backend error: {response.StatusCode}");
        //            }

        //            return await response.Content.ReadAsAsync<List<WeatherForecast>>();
        //        }
        //    }
        //}

        public async Task<string> PostClickInfoAsync(string userName, string userEmail)
        {
            var token = await AzureAuthHelper.Instance.GetAccessTokenAsync();

            var dto = new ClickInfoDTO
            {
                UserName = userName,
                UserEmail = userEmail,
                ClickTime = DateTime.UtcNow
                
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var request = new HttpRequestMessage(HttpMethod.Post, "/api/ClickInfo"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Content = content;

                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }


        public async Task<string> PostLogInfoAsync(string userName, string userEmail)
        {
            var token = await AzureAuthHelper.Instance.GetAccessTokenAsync();

            var dto = new LogInfoDTO
            {
                UserName = userName,
                UserEmail = userEmail,
                LoginTime = DateTime.UtcNow,
                LoginFrom = ClientAppDTO.Revit
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var request = new HttpRequestMessage(HttpMethod.Post, "/api/LogInfo"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Content = content;

                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }


    }
}
