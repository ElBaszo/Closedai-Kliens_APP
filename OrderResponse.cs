using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClosedAI
{
    public class OrderResponse
    {
        [JsonPropertyName("Content")]
        public List<OrderItem> Content { get; set; }
    }

    public class OrderItem
    {
        public long Id { get; set; }
        public string bvin { get; set; }
        public string OrderNumber { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalGrand { get; set; }
        public string StatusName { get; set; }
        public bool IsPlaced { get; set; }
        public string TimeOfOrderUtc { get; set; }
    }
}
