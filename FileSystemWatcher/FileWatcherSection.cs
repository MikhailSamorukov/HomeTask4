using System.Configuration;
using SystemFileWatcher.ConfigItems;

namespace SystemFileWatcher
{
    class FileWatcherSection : ConfigurationSection
    {
        [ConfigurationProperty("appName")]
        public string AppName
        {
            get => (string)this["appName"];
            set => this["appName"] = value;
        }
        [ConfigurationProperty("defaultFolder")]
        public DefaultFolderElement DefaultFolder
        {
            get => (DefaultFolderElement)this["defaultFolder"];
            set => this["defaultFolder"] = value;
        }
        [ConfigurationCollection(typeof(CultureElement), AddItemName = "culture")]
        [ConfigurationProperty("cultures")]
        public CulturesCollection CultureElement
        {
            get => (CulturesCollection)this["cultures"];
            set => this["cultures"] = value;
        }
        [ConfigurationCollection(typeof(FolderElement), AddItemName ="folder")]
        [ConfigurationProperty("folders")]
        public FoldersCollection Folders
        {
            get => (FoldersCollection)this["folders"];
            set => this["folders"] = value;
        }
        [ConfigurationCollection(typeof(FolderElement), AddItemName = "template")]
        [ConfigurationProperty("templates")]
        public TemplatesCollection Templates
        {
            get => (TemplatesCollection)this["templates"];
            set => this["templates"] = value;
        }
    }
}
