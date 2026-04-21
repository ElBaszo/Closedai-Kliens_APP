using System.Text.Json.Serialization;

namespace ClosedAI
{
    public class ProductResponse
    {
        [JsonPropertyName("Bvin")]
        public string Bvin { get; set; }

        [JsonPropertyName("Sku")]
        public string Sku { get; set; }

        [JsonPropertyName("ProductName")]
        public string ProductName { get; set; }
    }

    public class SingleProductResponse
    {
        [JsonPropertyName("Content")]
        public ProductResponse Content { get; set; }
    }
}