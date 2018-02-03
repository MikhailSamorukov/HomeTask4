using System.Configuration;

namespace SystemFileWatcher.ConfigItems
{
    class CultureElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey =true)]
        public string Name
        {
            get => (string)this["name"];
        }
        [ConfigurationProperty("value")]
        public string Value
        {
            get => (string)this["value"];
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
