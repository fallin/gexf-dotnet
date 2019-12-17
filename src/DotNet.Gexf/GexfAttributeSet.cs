using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfAttributeSet : HashSet<GexfAttribute>
    {
        public GexfClassType Class { get; }

        public GexfAttributeSet(GexfClassType @class) : base(GexfAttribute.IdComparer)
        {
            Class = @class;
        }

        public void AddRange(IEnumerable<GexfAttribute> attributes)
        {
            foreach (GexfAttribute attribute in attributes)
            {
                Add(attribute);
            }
        }

        public XElement Render(GexfXml xml)
        {
            XElement element = null;

            if (this.Any())
            {
                element = xml.Gexf.Element("attributes",
                    xml.Attribute("class", Class)
                );

                foreach (GexfAttribute attribute in this)
                {
                    element.Add(attribute.Render(xml));
                }
            }

            return element;
        }
    }
}