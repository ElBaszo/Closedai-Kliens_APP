using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ClosedAI
{
    public class ApiService
    {
        private string baseUrl = "http://40.67.243.80/DesktopModules/Hotcakes/API/rest/v1/";
        private string apiKey = "1-765e8551-eb7f-4bc2-bf9f-b28e56a1ebad";

        public async Task<string> GetOrders()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = baseUrl + "orders?key=" + apiKey;
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
