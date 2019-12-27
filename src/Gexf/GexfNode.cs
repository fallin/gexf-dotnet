using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Gexf
{
    [DebuggerDisplay("Id={Id}")]
    public class GexfNode : IIdentifiable<GexfId>, IExtensionPropertyContainer
    {
        private readonly Lazy<GexfAttributeValueSet> _attrValues;
        private readonly Lazy<GexfExtensionPropertySet> _extensionProperties;

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
            _extensionProperties = new Lazy<GexfExtensionPropertySet>(() => new GexfExtensionPropertySet());
        }

        public virtual XElement ToXml(GexfXml xml, GexfGraph graph)
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

            if (_extensionProperties.IsValueCreated)
            {
                _extensionProperties.Value.WriteTo(xml, element);
            }

            return element;
        }

        public GexfExtensionPropertySet ExtensionProperties => _extensionProperties.Value;
    }
}
