using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Text.Json.Nodes;


namespace ClosedAI
{
    public class ApiService
    {
        private HttpClient client = new HttpClient();
        private readonly string baseUrl = EnvConfig.Require("CLOSEDAI_API_BASE_URL").TrimEnd('/') + "/";
        private readonly string apiKey = EnvConfig.Require("CLOSEDAI_API_KEY");

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

                if (result == null || result.Content == null)
                    return new List<OrderItem>();

                return result.Content;
            }
        }

        public async Task<string> GetRawOrdersJson()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = baseUrl + "orders?key=" + apiKey;
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> GetOrderDetails(string bvin)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = baseUrl + "orders/" + bvin + "?key=" + apiKey;
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<List<OrderDetailItem>> GetOrderDetailsItems(string bvin)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = baseUrl + "orders/" + bvin + "?key=" + apiKey;

                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<OrderDetailResponse>(json, options);

                if (result != null && result.Content != null && result.Content.Items != null)
                    return result.Content.Items;

                return new List<OrderDetailItem>();
            }
        }
        /*
        public async Task<ProductInventoryResponse> GetProductInventory(string productId)
        {
            string url = $"{baseUrl}ProductInventoryFindForProduct?productBvin={productId}&key={apiKey}";

            HttpResponseMessage response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Inventory lekérés hiba:\n" + json);
                return null;
            }

            if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
            {
                MessageBox.Show("Az inventory lekérés nem JSON-t adott vissza.");
                return null;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                return JsonSerializer.Deserialize<ProductInventoryResponse>(json, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Inventory parse hiba:\n" + ex.Message);
                return null;
            }
        }
        */
        /*
        public async Task<ProductInventoryResponse> UpdateProductInventory(ProductInventoryResponse inventory)
        {
            string url = $"{baseUrl}ProductInventoryUpdate?key={apiKey}";

            string json = JsonSerializer.Serialize(inventory);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            string responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Inventory mentés hiba:\n" + responseJson);
                return null;
            }

            if (string.IsNullOrWhiteSpace(responseJson) || responseJson.TrimStart().StartsWith("<"))
            {
                MessageBox.Show("Az inventory mentés nem JSON-t adott vissza.");
                return null;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                return JsonSerializer.Deserialize<ProductInventoryResponse>(responseJson, options);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Inventory mentés parse hiba:\n" + ex.Message);
                return null;
            }
        }
        */
        public async Task<List<ProductResponse>> GetAllProducts()
        {
            string url = $"{baseUrl}products?key={apiKey}";

            HttpResponseMessage response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(json);
                return new List<ProductResponse>();
            }

            if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
            {
                MessageBox.Show("HIBA: Nem JSON jött vissza!");
                return new List<ProductResponse>();
            }

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    if (!doc.RootElement.TryGetProperty("Content", out JsonElement content))
                    {
                        MessageBox.Show("A JSON-ben nincs Content mező.");
                        return new List<ProductResponse>();
                    }

                    // 1. ha a Content maga egy tömb
                    if (content.ValueKind == JsonValueKind.Array)
                    {
                        var products = JsonSerializer.Deserialize<List<ProductResponse>>(content.GetRawText(),
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        return products ?? new List<ProductResponse>();
                    }

                    // 2. ha a Content objektum, és benne van Items
                    if (content.ValueKind == JsonValueKind.Object && content.TryGetProperty("Items", out JsonElement items))
                    {
                        var products = JsonSerializer.Deserialize<List<ProductResponse>>(items.GetRawText(),
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        return products ?? new List<ProductResponse>();
                    }

                    // 3. ha a Content objektum, és benne van Products
                    if (content.ValueKind == JsonValueKind.Object && content.TryGetProperty("Products", out JsonElement productsElement))
                    {
                        var products = JsonSerializer.Deserialize<List<ProductResponse>>(productsElement.GetRawText(),
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        return products ?? new List<ProductResponse>();
                    }

                    MessageBox.Show(content.GetRawText());
                    return new List<ProductResponse>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a termékek feldolgozásánál: " + ex.Message);
                return new List<ProductResponse>();
            }
        }

        public async Task<ProductResponse> GetProductBySku(string sku)
        {
            string encodedSku = Uri.EscapeDataString(sku);
            string url = $"{baseUrl}ProductsFindBySku/{encodedSku}?key={apiKey}";

            HttpResponseMessage response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(json);
                return null;
            }

            if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
            {
                MessageBox.Show("Nem JSON jött vissza, hanem HTML hibapage.");
                return null;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var wrappedResult = JsonSerializer.Deserialize<SingleProductResponse>(json, options);

                if (wrappedResult != null && wrappedResult.Content != null)
                    return wrappedResult.Content;
            }
            catch
            {
            }

            try
            {
                return JsonSerializer.Deserialize<ProductResponse>(json, options);
            }
            catch
            {
                MessageBox.Show("A termék válasza nem értelmezhető.");
                return null;
            }
        }

        public async Task<ProductInventoryResponse> GetInventoryByProductBvin(string productBvin)
        {
            string url = baseUrl + "productinventory?productBvin=" + productBvin + "&key=" + apiKey;

            HttpResponseMessage response = await client.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                return null;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<ProductInventoryListResponse>(json, options);

            if (result == null || result.Content == null)
                return null;

            return result.Content
                .FirstOrDefault(x => x.ProductBvin == productBvin);
        }
        public async Task<bool> SaveInventory(string inventoryBvin, string productBvin, string variantId, int quantity)
        {
            string url = baseUrl + "productinventory?key=" + apiKey;

            var data = new
            {
                Bvin = inventoryBvin,
                ProductBvin = productBvin,
                VariantId = variantId,
                QuantityOnHand = quantity,
                QuantityReserved = 0,
                LowStockPoint = 0,
                OutOfStockPoint = 0
            };

            string json = JsonSerializer.Serialize(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            string responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(responseText);
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateProductPrice(string productBvin, decimal newPrice)
        {
            string getUrl = baseUrl + "products?key=" + apiKey;

            HttpResponseMessage getResponse = await client.GetAsync(getUrl);
            string getJson = await getResponse.Content.ReadAsStringAsync();

            JsonNode root = JsonNode.Parse(getJson);

            JsonArray products = root["Content"]["Products"].AsArray();

            JsonObject selectedProduct = null;

            foreach (JsonNode item in products)
            {
                if (item["Bvin"] != null && item["Bvin"].ToString() == productBvin)
                {
                    selectedProduct = item.AsObject();
                    break;
                }
            }

            if (selectedProduct == null)
            {
                MessageBox.Show("Nem található a kiválasztott termék.");
                return false;
            }

            selectedProduct["SitePrice"] = newPrice;
            selectedProduct["ListPrice"] = newPrice;
            selectedProduct["CreationDateUtc"] = "2026-03-15T00:00:00";

            string postUrl = baseUrl + "products?key=" + apiKey;

            string postJson = selectedProduct.ToJsonString();

            StringContent content = new StringContent(postJson, Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await client.PostAsync(postUrl, content);
            string responseText = await postResponse.Content.ReadAsStringAsync();

            if (responseText.Contains("EXCEPTION"))
            {
                MessageBox.Show("API hiba:\n" + responseText);
                return false;
            }

            return true;
        }

        public async Task<string> GetCategoryForProduct(string productBvin)
        {
            string url =
                baseUrl +
                "categories?productBvin=" +
                productBvin +
                "&key=" +
                apiKey;

            HttpResponseMessage response =
                await client.GetAsync(url);

            string json =
                await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
            {
                return "Unknown";
            }

            JsonDocument doc =
                JsonDocument.Parse(json);

            if (doc.RootElement
                  .GetProperty("Content")
                  .GetArrayLength() > 0)
            {
                return doc.RootElement
                    .GetProperty("Content")[0]
                    .GetProperty("Name")
                    .GetString();
            }

            return "Unknown";
        }
    }
}
