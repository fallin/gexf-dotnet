using System;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfMeta
    {
        public DateTimeOffset LastModified { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }

        public GexfMeta()
        {
            LastModified = DateTimeOffset.Now;
        }

        public XElement Render(GexfXml xml)
        {
            var element = xml.Gexf.Element("meta",
                xml.Attribute("lastmodifieddate", LastModified),
                xml.Gexf.Element("creator", Creator),
                xml.Gexf.Element("description", Description),
                xml.Gexf.Element("keywords", Keywords)
            );
            return element;
        }
    }
}