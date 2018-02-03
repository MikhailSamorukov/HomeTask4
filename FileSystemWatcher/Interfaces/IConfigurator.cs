using System.Collections.Generic;
using SystemFileWatcher.ConfigItems;

namespace SystemFileWatcher.Abstract
{
    interface IConfigurator
    {
       string GetCulture(string cultureName);
       IEnumerable<string> GetWatherFolders();
       IEnumerable<TemplateElement> GetRules();
       string GetDefaultFolder();
    }
}
