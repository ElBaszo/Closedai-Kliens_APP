using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace ClosedAI
{
    public class ApiService
    {
        private string baseUrl = "http://40.67.243.80/DesktopModules/Hotcakes/API/rest/v1/";
        private string apiKey = "1-765e8551-eb7f-4bc2-bf9f-b28e56a1ebad";

        public async Task<List<OrderItem>> GetOrders()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = baseUrl + "orders?key=" + apiKey;

                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<OrderResponse>(json, options);

                return result.Content;
            }
        }
    }
}
