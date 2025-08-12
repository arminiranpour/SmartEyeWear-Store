using System.Collections.Generic;

namespace SmartEyewearStore.Models.Catalog
{
    public class Material
    {
        public int MaterialId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<FrameSpecs> FrameSpecs { get; set; } = new HashSet<FrameSpecs>();
    }
}