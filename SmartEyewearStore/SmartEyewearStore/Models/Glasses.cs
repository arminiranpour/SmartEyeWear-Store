using Microsoft.EntityFrameworkCore;

namespace SmartEyewearStore.Models
{
    public class Glasses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FrameShape { get; set; }
        public string Color { get; set; }
        public string Style { get; set; }
        public string Usage { get; set; }

        [Precision(10, 2)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
