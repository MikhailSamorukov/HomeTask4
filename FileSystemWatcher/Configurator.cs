using System.Collections.Generic;
using System.Linq;
using SystemFileWatcher.Abstract;
using System.Configuration;
using SystemFileWatcher.ConfigItems;

namespace SystemFileWatcher
{
    class Configurator : IConfigurator
    {
        readonly FileWatcherSection _section;
        public Configurator()
        {
            _section = (FileWatcherSection)ConfigurationManager.GetSection("fileWathcerSection");
        }

        public string GetCulture(string cultureName)
        {
            return _section.CultureElement
                        .Cast<CultureElement>()
                        .Where(i=>i.Name == cultureName)
                        .Select(i=>i.Value)
                        .FirstOrDefault();
        }

        public IEnumerable<TemplateElement> GetRules()
        {
            return _section.Templates.Cast<TemplateElement>();
        }

        public IEnumerable<string> GetWatherFolders()
        {
            return from FolderElement item in _section.Folders select item.Path;
        }

        public string GetDefaultFolder()
        {
           return _section.DefaultFolder.Path;
        }
    }
}
