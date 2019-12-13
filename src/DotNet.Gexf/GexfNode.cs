using System;
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
            Id = id;

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
    }
}
