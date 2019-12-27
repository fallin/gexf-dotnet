using System;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfAttributeValue : IIdentifiable<GexfId>
    {
        public GexfId For { get; }
        public object Value { get; }

        GexfId IIdentifiable<GexfId>.Id => For;

        public GexfAttributeValue(GexfId @for, object value)
        {
            For = @for ?? throw new ArgumentNullException(nameof(@for));
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