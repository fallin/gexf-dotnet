using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Gexf
{
    [DebuggerDisplay("Id={Id}, Source={Source}, Target={Target}")]
    public class GexfEdge : IIdentifiable<GexfId>, IExtensionPropertyContainer
    {
        private readonly Lazy<GexfAttributeValueSet> _attrValues;
        private readonly Lazy<GexfExtensionPropertySet> _extensionProperties;

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
            _extensionProperties = new Lazy<GexfExtensionPropertySet>(() => new GexfExtensionPropertySet());
        }

        public virtual XElement Render(GexfOutput output, GexfGraph graph)
        {
            var element = output.Gexf.Element("edge",
                output.Attribute("id", Id),
                output.Attribute("source", Source),
                output.Attribute("target", Target),

                // The label attribute is optional
                output.When(() => !string.IsNullOrEmpty(Label),
                    () => output.Attribute("label", Label)),
                
                output.When(() => !Weight.Equals(1.0f),
                    () => output.Attribute("weight", Weight)),

                output.DefaultedAttribute("type", Type, graph.DefaultedEdgeType ?? GexfEdgeType.Undirected),

                output.When(() => _attrValues.IsValueCreated && AttrValues.Any(),
                    () => AttrValues.Render(output))
            );

            if (_extensionProperties.IsValueCreated)
            {
                _extensionProperties.Value.WriteTo(output, element);
            }

            return element;
        }

        public GexfExtensionPropertySet ExtensionProperties => _extensionProperties.Value;
    }
}