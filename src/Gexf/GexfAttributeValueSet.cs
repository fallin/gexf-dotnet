using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfAttributeValueSet : GexfHashSet<GexfAttributeValue>
    {
        public XElement Render(GexfXml xml)
        {
            XElement element = null;

            if (this.Any())
            {
                // ReSharper disable once StringLiteralTypo
                element = xml.Gexf.Element("attvalues");

                foreach (GexfAttributeValue attributeValue in this)
                {
                    element.Add(attributeValue.Render(xml));
                }
            }

            return element;
        }
    }
}