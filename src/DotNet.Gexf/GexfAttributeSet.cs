using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfAttributeSet : GexfHashSet<GexfAttribute>
    {
        public GexfClassType Class { get; }

        public GexfAttributeSet(GexfClassType @class)
        {
            Class = @class;
        }

        public XElement ToXml(GexfXml xml)
        {
            XElement element = null;

            if (this.Any())
            {
                element = xml.Gexf.Element("attributes",
                    xml.Attribute("class", Class)
                );

                foreach (GexfAttribute attribute in this)
                {
                    element.Add(attribute.ToXml(xml));
                }
            }

            return element;
        }
    }
}