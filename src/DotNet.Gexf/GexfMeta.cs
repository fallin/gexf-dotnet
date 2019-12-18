using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfMeta
    {
        public DateTimeOffset LastModified { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public HashSet<string> Keywords { get; }

        public GexfMeta()
        {
            LastModified = DateTimeOffset.Now;
            Keywords = new HashSet<string>();
        }

        public XElement Render(GexfXml xml)
        {
            var element = xml.Gexf.Element("meta",
                xml.Attribute("lastmodifieddate", LastModified),
                xml.Gexf.Element("creator", Creator),
                xml.Gexf.Element("description", Description),
                xml.Gexf.Element("keywords", string.Join(",", Keywords))
            );
            return element;
        }
    }
}