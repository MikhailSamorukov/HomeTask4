using System.Configuration;

namespace SystemFileWatcher.ConfigItems
{
    class TemplateElement : ConfigurationElement
    {
        [ConfigurationProperty("regularExpression", IsKey = true)]
        public string Expression
        {
            get => (string)this["regularExpression"];
        }

        [ConfigurationProperty("destinationFolder")]
        public string DestinationFolder
        {
            get => (string)this["destinationFolder"];
        }

        [ConfigurationProperty("addSerialNumber")]
        public bool AddSerialNumber
        {
            get => (bool)this["addSerialNumber"];
        }

        [ConfigurationProperty("addDateTransfer")]
        public bool AddDateTransfer
        {
            get => (bool)this["addDateTransfer"];
        }
    }

    class TemplatesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TemplateElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TemplateElement)element).Expression;
        }
    }
}
