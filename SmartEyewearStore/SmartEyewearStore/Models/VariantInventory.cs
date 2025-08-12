namespace SmartEyewearStore.Models.Catalog
{
    public class VariantInventory
    {
        public int VariantId { get; set; }
        public int QtyOnHand { get; set; }
        public bool? Backorderable { get; set; }

        public ProductVariant Variant { get; set; } = null!;
    }
}