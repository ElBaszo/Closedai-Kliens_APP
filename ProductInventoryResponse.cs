namespace ClosedAI
{
    public class ProductInventoryResponse
    {
        public string Bvin { get; set; }
        public string ProductBvin { get; set; }
        public string VariantId { get; set; }
        public int QuantityOnHand { get; set; }
        public int QuantityReserved { get; set; }
        public int LowStockPoint { get; set; }
        public int OutOfStockPoint { get; set; }
    }
}