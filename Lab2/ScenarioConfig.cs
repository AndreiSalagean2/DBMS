using System.Collections.Specialized;
using System.Configuration;

public class ScenarioConfig
{
    public string Caption { get; set; }
    public string ParentSelect { get; set; }
    public string ChildSelect { get; set; }
    public string ParentKey { get; set; }
    public string ChildForeignKey { get; set; }
    public string ChildInsertProc { get; set; }
    public string ChildUpdateProc { get; set; }
    public string ChildDeleteProc { get; set; }

    public static ScenarioConfig Load()
    {
        string scenarioKey = ConfigurationManager.AppSettings["ActiveScenario"];
        NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("masterDetail");

        string prefix = scenarioKey + ".";

        return new ScenarioConfig
        {
            Caption = config[prefix + "Caption"],
            ParentSelect = config[prefix + "ParentSelect"],
            ChildSelect = config[prefix + "ChildSelect"],
            ParentKey = config[prefix + "ParentKey"],
            ChildForeignKey = config[prefix + "ChildForeignKey"],
            ChildInsertProc = config[prefix + "ChildInsert"],
            ChildUpdateProc = config[prefix + "ChildUpdate"],
            ChildDeleteProc = config[prefix + "ChildDelete"]
        };
    }
}
