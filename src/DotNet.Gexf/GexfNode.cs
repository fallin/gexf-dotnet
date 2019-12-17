using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    [DebuggerDisplay("Id={Id}")]
    public class GexfNode
    {
        private readonly Lazy<GexfAttributeValueList> _attrValues;

        public GexfId Id { get; }
        public string Label { get; set; }
        public GexfAttributeValueList AttrValues => _attrValues.Value;

        public GexfNode(GexfId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));

            _attrValues = new Lazy<GexfAttributeValueList>(() => new GexfAttributeValueList());
        }

        public virtual XElement Render(GexfXml xml, GexfGraph graph)
        {
            var element = xml.Gexf.Element("node",
                xml.Attribute("id", Id),
                xml.Attribute("label", Label),

                xml.When(() => Id.Type != graph.IdType,
                    () => xml.Attribute("type", Id.Type)),
                xml.When(() => _attrValues.IsValueCreated && AttrValues.Any(),
                    () => AttrValues.Render(xml))
            );

            return element;
        }

        private sealed class IdEqualityComparer : IEqualityComparer<GexfNode>
        {
            public bool Equals(GexfNode x, GexfNode y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(GexfNode obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        public static IEqualityComparer<GexfNode> IdComparer { get; } = new IdEqualityComparer();
    }
}
