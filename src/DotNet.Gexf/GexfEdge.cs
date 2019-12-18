using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    [DebuggerDisplay("Id={Id}, Source={Source}, Target={Target}")]
    public class GexfEdge : IIdentifiable<GexfId>
    {
        private readonly Lazy<GexfAttributeValueSet> _attrValues;

        public GexfId Id { get; }
        public GexfId Source { get; }
        public GexfId Target { get; }
        public GexfFloat Weight { get; set; }

        public string Label { get; set; }
        public GexfEdgeType? Type { get; set; }
        public GexfAttributeValueSet AttrValues => _attrValues.Value;

        public GexfEdge(GexfId id, GexfId source, GexfId target)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Target = target ?? throw new ArgumentNullException(nameof(source));
            Weight = 1.0f;

            _attrValues = new Lazy<GexfAttributeValueSet>(() => new GexfAttributeValueSet());
        }

        public virtual XElement Render(GexfXml xml, GexfGraph graph)
        {
            var element = xml.Gexf.Element("edge",
                xml.Attribute("id", Id),
                xml.Attribute("source", Source),
                xml.Attribute("target", Target),

                // The label attribute is optional
                xml.When(() => !string.IsNullOrEmpty(Label),
                    () => xml.Attribute("label", Label)),
                
                xml.When(() => !Weight.Equals(1.0f),
                    () => xml.Attribute("weight", Weight)),

                xml.When(() => Type.HasValue && Type != graph.DefaultEdgeType,
                    () => xml.Attribute("type", Type)),

                xml.When(() => _attrValues.IsValueCreated && AttrValues.Any(),
                    () => AttrValues.Render(xml))
            );

            return element;
        }
    }
}