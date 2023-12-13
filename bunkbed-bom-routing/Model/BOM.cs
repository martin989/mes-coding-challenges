using System.Collections;

namespace bunkbed_bom_routing.Model
{
    public class bunkbed_bom_BOM
    {
        public required string Description { get; set; }
        public required int Quantity { get; set; }
        public int Step { get; set; }
        public required string Source { get; set; }
        public required List<bunkbed_bom_BOM> BOM { get; set; }
    }
}