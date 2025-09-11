using System.Collections.Generic;

namespace SmartEyewearStore.Models
{
    public class CartPageViewModel
    {
        public List<Line> Items { get; set; } = new();
        public int SubtotalCents { get; set; }
        public string Currency { get; set; } = "USD";
        public string? ErrorMessage { get; set; }

        public class Line
        {
            public int CartItemId { get; set; }
            public int VariantId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public int UnitPriceCents { get; set; }
            public int Qty { get; set; }
            public int LineTotalCents => UnitPriceCents * Qty;
        }
    }
}
