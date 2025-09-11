using System;
using System.Collections.Generic;

namespace SmartEyewearStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public string? GuestId { get; set; }
        public int CartId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string BillingAddress1 { get; set; } = string.Empty;
        public string? BillingAddress2 { get; set; }
        public string BillingCity { get; set; } = string.Empty;
        public string BillingState { get; set; } = string.Empty;
        public string BillingPostalCode { get; set; } = string.Empty;
        public string BillingCountry { get; set; } = "US";

        public bool ShipToDifferent { get; set; }
        public string? ShippingFullName { get; set; }
        public string? ShippingPhone { get; set; }
        public string? ShippingAddress1 { get; set; }
        public string? ShippingAddress2 { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingState { get; set; }
        public string? ShippingPostalCode { get; set; }
        public string? ShippingCountry { get; set; }

        public int SubtotalCents { get; set; }
        public int ShippingCents { get; set; }
        public int TaxCents { get; set; }
        public int TotalCents { get; set; }

        public Cart Cart { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = new();
    }
}
