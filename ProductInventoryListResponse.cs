using System.Collections.Generic;

namespace ClosedAI
{
    public class ProductInventoryListResponse
    {
        public List<ProductInventoryResponse> Content { get; set; }
        public List<object> Errors { get; set; }
    }
}