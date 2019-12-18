using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfAttributeValue
    {
        public GexfId For { get; }
        public object Value { get; }

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

        private sealed class IdEqualityComparer : IEqualityComparer<GexfAttributeValue>
        {
            public bool Equals(GexfAttributeValue x, GexfAttributeValue y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.For.Equals(y.For);
            }

            public int GetHashCode(GexfAttributeValue obj)
            {
                return obj.For.GetHashCode();
            }
        }

        public static IEqualityComparer<GexfAttributeValue> ForComparer { get; } = new IdEqualityComparer();
    }
}