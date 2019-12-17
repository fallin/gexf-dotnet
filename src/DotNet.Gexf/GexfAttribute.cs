using System.Collections.Generic;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfAttribute
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

        public XElement Render(GexfXml xml)
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

        private sealed class IdEqualityComparer : IEqualityComparer<GexfAttribute>
        {
            public bool Equals(GexfAttribute x, GexfAttribute y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(GexfAttribute obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        public static IEqualityComparer<GexfAttribute> IdComparer { get; } = new IdEqualityComparer();
    }
}