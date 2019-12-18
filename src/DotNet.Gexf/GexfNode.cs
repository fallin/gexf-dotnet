using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public class GexfHierarchicalNode : GexfNode
    {
        public GexfNodeSet Nodes { get; }
        public GexfEdgeSet Edges { get; }

        public GexfHierarchicalNode(GexfId id) 
            : this(id, string.Empty)
        {
        }

        public GexfHierarchicalNode(GexfId id, string label, params IIdentifiable<GexfId>[] content) 
            : base(id, label)
        {
            Nodes = new GexfNodeSet(content.OfType<GexfNode>());
            Edges = new GexfEdgeSet(content.OfType<GexfEdge>());
        }

        public override XElement Render(GexfXml xml, GexfGraph graph)
        {
            XElement element = base.Render(xml, graph);

            if (Nodes.Any())
            {
                element.Add(Nodes.Render(xml, graph));
            }

            if (Edges.Any())
            {
                element.Add(Edges.Render(xml, graph));
            }
           
            return element;
        }
    }

    public class GexfParentedNode : GexfNode
    {
        public GexfId Parent { get; set; }

        public GexfParentedNode(GexfId id) : base(id)
        {
        }

        public override XElement Render(GexfXml xml, GexfGraph graph)
        {
            XElement element = base.Render(xml, graph);

            if (Parent != null)
            {
                element.Add(xml.Attribute("pid", Parent));
            }

            return element;
        }
    }

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
