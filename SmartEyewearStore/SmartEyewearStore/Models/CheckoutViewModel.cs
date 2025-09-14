using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartEyewearStore.Models
{
    public class CheckoutViewModel
    {
        [Required] public string FullName { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required] public string Phone { get; set; } = "";
        [Required] public string BillingAddress1 { get; set; } = "";
        public string? BillingAddress2 { get; set; }
        [Required] public string BillingCity { get; set; } = "";
        [Required] public string BillingState { get; set; } = "";
        [Required] public string BillingPostalCode { get; set; } = "";
        public string BillingCountry { get; set; } = "US";


        public int SubtotalCents { get; set; }
        public int ShippingCents { get; set; }
        public int TaxCents { get; set; }
        public int TotalCents { get; set; }

        public List<Line> Lines { get; set; } = new();
        public class Line
        {
            public int CartItemId { get; set; }
            public int VariantId { get; set; }
            public string ProductName { get; set; } = "";
            public string? ImageUrl { get; set; }
            public int UnitPriceCents { get; set; }
            public int Qty { get; set; }
            public int LineTotalCents { get; set; }
        }
    }
}
