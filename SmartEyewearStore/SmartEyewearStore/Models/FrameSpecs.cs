using System;

namespace SmartEyewearStore.Models.Catalog
{
    public class FrameSpecs
    {
        public int ProductId { get; set; }
        public int? MaterialId { get; set; }
        public int? ShapeId { get; set; }
        public int? RimStyleId { get; set; }
        public decimal? WeightG { get; set; }
        public string? Gender { get; set; }
        public string? Notes { get; set; }

        public Product Product { get; set; } = null!;
        public Material? Material { get; set; }
        public Shape? Shape { get; set; }
        public RimStyle? RimStyle { get; set; }
    }
}