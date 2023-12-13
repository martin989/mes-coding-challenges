using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http.Headers;
using bunkbed_bom_routing.Model;
namespace bunkbed_bom_routing.Helper
{
    public class ObjectCreate
    {
        public static List<bunkbed_bom_BOM> CreateBOMObject(string fileName)
        {
            try
            {
                string jsonText = File.ReadAllText(fileName);
                var data = JsonConvert.DeserializeObject<bunkbed_bom_BOM>(jsonText);
                return data != null ? new List<bunkbed_bom_BOM>() { data } : new List<Model.bunkbed_bom_BOM>();
            }
            catch (System.Exception ex)
            {
                throw new Exception("Error in CreateBOMObject " + ex.Message);
            }
        }

        public static List<bunkbed_bom_routing_item> CreateRoutingObject(string fileName)
        {
            try
            {
                string jsonText = File.ReadAllText(fileName);
                var data = JsonConvert.DeserializeObject<List<bunkbed_bom_routing_item>>(jsonText);
                return data != null ? data : new List<bunkbed_bom_routing_item>();
            }
            catch (System.Exception ex)
            {
                throw new Exception("Error in CreateRoutingObject " + ex.Message);
            }
        }
    }

}