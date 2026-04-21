using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClosedAI
{
    public class OrderDetailResponse
    {
        [JsonPropertyName("Content")]
        public OrderDetailContent Content { get; set; }
    }

    public class OrderDetailContent
    {
        [JsonPropertyName("Items")]
        public List<OrderDetailItem> Items { get; set; }
    }

    public class OrderDetailItem
    {
        [JsonPropertyName("ProductId")]
        public string ProductId { get; set; }

        [JsonPropertyName("ProductName")]
        public string ProductName { get; set; }

        [JsonPropertyName("ProductSku")]
        public string ProductSku { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("LineTotal")]
        public decimal LineTotal { get; set; }
    }
}