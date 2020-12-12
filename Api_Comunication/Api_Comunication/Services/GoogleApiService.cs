using Api_Comunication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Api_Comunication.Services
{
    public class GoogleApiService : IGoogleApiService
    {
        static string _googleMapsKey;

        public static void Initialize()
        {
            _googleMapsKey = "YOUR_API_KEY";
        }

        public async Task<Root> GetDistance(string originPlace, string destinationPlace)
        {
            Initialize();
            Root root = new Root();
            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/distancematrix/json?origins={originPlace}&destinations={destinationPlace}&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        root = await Task.Run(() =>
                           JsonConvert.DeserializeObject<Root>(json)
                        ).ConfigureAwait(false);
                    }
                }
            }
            return root;
        }

        private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
