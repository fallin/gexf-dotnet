using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfAttributeValueSet : HashSet<GexfAttributeValue>
    {
        public GexfAttributeValueSet() : base(GexfAttributeValue.ForComparer)
        {
            
        }

        public void AddRange(IEnumerable<GexfAttributeValue> values)
        {
            foreach (GexfAttributeValue value in values)
            {
                Add(value);
            }
        }

        public XElement Render(GexfXml xml)
        {
            XElement element = null;

            if (this.Any())
            {
                // ReSharper disable once StringLiteralTypo
                element = xml.Gexf.Element("attvalues"
                );

                foreach (GexfAttributeValue attributeValue in this)
                {
                    element.Add(attributeValue.Render(xml));
                }
            }

            return element;
        }
    }
}