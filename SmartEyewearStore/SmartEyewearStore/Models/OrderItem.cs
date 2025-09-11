namespace SmartEyewearStore.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int VariantId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? VariantLabel { get; set; }
        public int UnitPriceCents { get; set; }
        public int Qty { get; set; }

        public Order Order { get; set; } = null!;
    }
}
