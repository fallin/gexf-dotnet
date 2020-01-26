using System.Linq;
using System.Xml.Linq;

namespace Gexf
{
    public sealed class GexfAttributeValueSet : GexfHashSet<GexfAttributeValue>
    {
        public XElement Render(GexfOutput output)
        {
            XElement element = null;

            if (this.Any())
            {
                // ReSharper disable once StringLiteralTypo
                element = output.Gexf.Element("attvalues");

                foreach (GexfAttributeValue attributeValue in this)
                {
                    element.Add(attributeValue.Render(output));
                }
            }

            return element;
        }
    }
}