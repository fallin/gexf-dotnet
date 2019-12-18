using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    [DebuggerDisplay("Id={Id}")]
    public class GexfNode : IIdentifiable<GexfId>
    {
        private readonly Lazy<GexfAttributeValueSet> _attrValues;

        public GexfId Id { get; }
        public string Label { get; set; }
        public GexfAttributeValueSet AttrValues => _attrValues.Value;

        public GexfNode(GexfId id) : this(id, string.Empty)
        {
        }

        public GexfNode(GexfId id, string label)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Label = label;

            _attrValues = new Lazy<GexfAttributeValueSet>(() => new GexfAttributeValueSet());
        }

        public virtual XElement Render(GexfXml xml, GexfGraph graph)
        {
            string RequiredLabel() => string.IsNullOrWhiteSpace(Label) ? $"{Id}" : Label;

            var element = xml.Gexf.Element("node",
                xml.Attribute("id", Id),

                // The label attribute is required
                xml.Attribute("label", RequiredLabel()),

                xml.When(() => Id.Type != graph.IdType,
                    () => xml.Attribute("type", Id.Type)),
                xml.When(() => _attrValues.IsValueCreated && AttrValues.Any(),
                    () => AttrValues.Render(xml))
            );

            return element;
        }
    }
}
