using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bunkbed_bom_routing.Model
{
    public class bunkbed_bom_routing_item
    {
        public required int Step { get; set; }
        public required string Description { get; set; }
        public required int TaktTime { get; set; }
    }
}
