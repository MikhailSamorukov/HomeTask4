using System.Configuration;

namespace SystemFileWatcher.ConfigItems
{
    class TemplateElement : ConfigurationElement
    {
        [ConfigurationProperty("regularExpression", IsKey = true)]
        public string Expression
        {
            get => (string)this["regularExpression"];
            set => this["regularExpression"] = value;
        }

        [ConfigurationProperty("destinationFolder")]
        public string DestinationFolder
        {
            get => (string)this["destinationFolder"];
            set => this["destinationFolder"] = value;
        }

        [ConfigurationProperty("addSerialNumber")]
        public bool AddSerialNumber
        {
            get => (bool)this["addSerialNumber"];
            set => this["addSerialNumber"] = value;
        }

        [ConfigurationProperty("addDateTransfer")]
        public bool AddDateTransfer
        {
            get => (bool)this["addDateTransfer"];
            set => this["addDateTransfer"] = value;
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
