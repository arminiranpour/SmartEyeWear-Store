namespace SmartEyewearStore.Models.Catalog
{
    public class RatingSummary
    {
        public int ProductId { get; set; }
        public decimal? AvgRating { get; set; }
        public int? RatingCount { get; set; }

        public Product Product { get; set; } = null!;
    }
}