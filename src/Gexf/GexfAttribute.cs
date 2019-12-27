using System.Collections.Generic;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfAttribute : IIdentifiable<GexfId>
    {
        public GexfId Id { get; }
        public string Title { get; set; }
        public GexfDataType Type { get; set; }

        public object Default { get; set; }
        public GexfOptions Options { get; set; }

        public GexfAttribute(GexfId id)
        {
            Id = id;
        }

        public XElement ToXml(GexfXml xml)
        {
            var element = xml.Gexf.Element("attribute",
                xml.Attribute("id", Id),
                xml.Attribute("title", Title),
                xml.Attribute("type", Type)
            );

            if (Default != null)
            {
                element.Add(xml.Gexf.Element("default"), Default);
            }

            if (Options != null)
            {
                element.Add(xml.Gexf.Element("options", Options));
            }

            return element;
        }
    }
}