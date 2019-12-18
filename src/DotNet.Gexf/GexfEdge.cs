using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    [DebuggerDisplay("Id={Id}, Source={Source}, Target={Target}")]
    public class GexfEdge
    {
        private readonly Lazy<GexfAttributeValueList> _attrValues;

        public GexfId Id { get; }
        public GexfId Source { get; set; }
        public GexfId Target { get; set; }
        public float? Weight { get; set; }

        public string Label { get; set; }
        public GexfEdgeType Type { get; set; }
        public GexfAttributeValueList AttrValues => _attrValues.Value;

        public GexfEdge(GexfId id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Type = GexfEdgeType.Undirected;

            _attrValues = new Lazy<GexfAttributeValueList>(() => new GexfAttributeValueList());
        }

        public virtual XElement Render(GexfXml xml, GexfGraph graph)
        {
            var element = xml.Gexf.Element("edge",
                xml.Attribute("id", Id),
                xml.Attribute("source", Source),
                xml.Attribute("target", Target),

                xml.When(() => !string.IsNullOrEmpty(Label),
                    () => xml.Attribute("label", Label)),
                
                xml.When(() => Weight.HasValue && !GexfFloat.Equal(Weight.Value, 1.0f),
                    () => xml.Attribute("weight", Weight)),

                xml.When(() => Type != graph.DefaultEdgeType,
                    () => xml.Attribute("type", Type)),

                xml.When(() => _attrValues.IsValueCreated && AttrValues.Any(),
                    () => AttrValues.Render(xml))
            );

            return element;
        }

        private sealed class IdEqualityComparer : IEqualityComparer<GexfEdge>
        {
            public bool Equals(GexfEdge x, GexfEdge y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(GexfEdge obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        public static IEqualityComparer<GexfEdge> IdComparer { get; } = new IdEqualityComparer();
    }
}