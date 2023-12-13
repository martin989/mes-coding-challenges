using bunkbed_bom_routing.Helper;
using bunkbed_bom_routing.Model;

//Variables
Dictionary<string, int> dictComponents = new Dictionary<string, int>();
Dictionary<int, int> dictTaktTime = new Dictionary<int, int>();
Dictionary<int, string> dictComponentsSteps = new Dictionary<int, string>();
int taktTimeTotal = 0;
string bomPath = Path.Combine(Directory.GetCurrentDirectory(), "bunkbed-bom.json");
var routingPath = Path.Combine(Directory.GetCurrentDirectory(), "bunkbed-routing.json");

//BOM file check
if (File.Exists(bomPath) & File.Exists(routingPath))
{
    List<bunkbed_bom_BOM> newBOM = ObjectCreate.CreateBOMObject(bomPath);
    var newRouting = ObjectCreate.CreateRoutingObject(routingPath);

    dictTaktTime = Helpers.CreateTaktTimeDict(newRouting);
    dictComponentsSteps = Helpers.CreateComponentsStepsDict(newRouting);

    foreach (bunkbed_bom_BOM item in newBOM)
    {
        GetBOMRecursively(item.BOM);
    }

    Helpers.WriteCSVFile(dictComponents);
    Console.WriteLine("\n\n\nTakt time check: \nOverall takt time: " + taktTimeTotal.ToString() + " minutes.\n\n");
    Console.WriteLine("Provided components check:");
    foreach (var item in dictComponentsSteps) { Console.WriteLine("Step " + item.Key + " " + item.Value + " has no provided components added."); }
}
else
{
    Console.WriteLine("BOM or Routing path does not exists. \n BOM path is: " + bomPath + "BOM path is: " + routingPath);
}


List<bunkbed_bom_BOM> GetBOMRecursively(List<bunkbed_bom_BOM> bom)
{
    foreach (bunkbed_bom_BOM item in bom)
    {
        if (item.BOM.Count > 0) { GetBOMRecursively(item.BOM); }
        if (item.Source == "Provided") { AddandRemoveFromDictionary(item.Description, item.Quantity, item.Step); };
        taktTimeTotal = taktTimeTotal + Helpers.GetTaktTime(item.Step, dictTaktTime);
    }
    return new List<bunkbed_bom_BOM>();
}

void AddandRemoveFromDictionary(string desc, int value, int step)
{
    if (dictComponents.ContainsKey(desc)) { dictComponents[desc] += value; }
    else { dictComponents.Add(desc, value); }
    if (dictComponentsSteps.ContainsKey(step)) { dictComponentsSteps.Remove(step); }
}





