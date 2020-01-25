using System;
using System.Xml.Linq;

namespace Gexf
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

        public XElement Render(GexfOutput output)
        {
            // ReSharper disable once StringLiteralTypo
            var element = output.Gexf.Element("attvalue",
                output.Attribute("for", For),
                output.Attribute("value", Value)
            );

            return element;
        }
    }
}