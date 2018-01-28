using System.Configuration;

namespace SystemFileWatcher.ConfigItems
{
    class CultureElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey =true)]
        public string Name
        {
            get => (string)this["name"];
            set => this["name"] = value;
        }
        [ConfigurationProperty("value")]
        public string Value
        {
            get => (string)this["value"];
            set => this["value"] = value;
        }
    }
    class CulturesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CultureElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CultureElement)element).Name;
        }
    }
}
