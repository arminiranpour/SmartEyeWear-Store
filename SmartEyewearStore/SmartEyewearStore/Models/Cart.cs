using System;
using System.Collections.Generic;
using SmartEyewearStore.Models.Catalog;

namespace SmartEyewearStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }           // برای کاربر لاگین
        public string? GuestId { get; set; }       // برای مهمان (از Session)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<CartItem> Items { get; set; } = new();
    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int VariantId { get; set; }
        public int Qty { get; set; }

        public Cart Cart { get; set; } = null!;
        public ProductVariant Variant { get; set; } = null!;
    }
}
