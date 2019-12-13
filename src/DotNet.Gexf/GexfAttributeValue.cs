using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfAttributeValue
    {
        public GexfId For { get; }
        public object Value { get; }

        public GexfAttributeValue(GexfId @for, object value)
        {
            For = @for;
            Value = value;
        }

        public XElement Render(GexfXml xml)
        {
            // ReSharper disable once StringLiteralTypo
            var element = xml.Gexf.Element("attvalue",
                xml.Attribute("for", For),
                xml.Attribute("value", Value)
            );

            return element;
        }
    }
}