using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using bunkbed_bom_routing.Model;

namespace bunkbed_bom_routing.Helper
{
    public class Helpers
    {

        public static bool WriteCSVFile(Dictionary<string, int> dictComponents)
        {
            try
            {
                using var writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "output.csv"));
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

                csv.WriteField("component");
                csv.WriteField("quantity");
                csv.NextRecord();
                foreach (var item in dictComponents)
                {
                    csv.WriteRecord(item);
                    csv.NextRecord();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                throw new Exception("Error in WriteCSVFile " + ex.Message);
            }
        }

        public static Dictionary<int, int> CreateTaktTimeDict(List<bunkbed_bom_routing_item> routingList)
        {
            try
            {
                Dictionary<int, int> dictTaktTime = new Dictionary<int, int>();
                foreach (bunkbed_bom_routing_item item in routingList)
                {
                    dictTaktTime.Add(item.Step, item.TaktTime);
                }
                return dictTaktTime;
            }
            catch (System.Exception ex)
            {
                throw new Exception("Error in CreateTaktTimeDict " + ex.Message);
            }
        }

        public static Dictionary<int, string> CreateComponentsStepsDict(List<bunkbed_bom_routing_item> routingList)
        {
            Dictionary<int, string> dictComponentsSteps = new Dictionary<int, string>();
            foreach (bunkbed_bom_routing_item item in routingList)
            {
                dictComponentsSteps.Add(item.Step, item.Description);
            }
            return dictComponentsSteps;
        }

        public static int GetTaktTime(int itemStep, Dictionary<int, int> dictTaktTime)
        {
            try
            {
                int value = 0;
                bool found = dictTaktTime.TryGetValue(itemStep, out value);
                if (found)
                {
                    return value;
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("Error in GetTaktTime " + ex.Message);
            }
            return -9999;
        }
    }

}