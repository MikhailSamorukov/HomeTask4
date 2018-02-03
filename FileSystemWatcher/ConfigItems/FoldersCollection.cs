using System.Configuration;

namespace SystemFileWatcher.ConfigItems
{
    class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsKey = true)]
        public string Path
        {
            get => (string)this["path"];
        }
    }

    class DefaultFolderElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsKey = true)]
        public string Path
        {
            get => (string)this["path"];
        }
    }

    class FoldersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderElement)element).Path;
        }
    }
}
