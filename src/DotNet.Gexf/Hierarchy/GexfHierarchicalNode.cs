using System.Linq;
using System.Xml.Linq;

namespace DotNet.Gexf.Hierarchy
{
    public class GexfHierarchicalNode : GexfNode
    {
        public GexfNodeSet Nodes { get; }
        public GexfEdgeSet Edges { get; }

        public GexfHierarchicalNode(GexfId id) 
            : this(id, string.Empty)
        {
        }

        public GexfHierarchicalNode(GexfId id, string label) 
            : base(id, label)
        {
            Nodes = new GexfNodeSet();
            Edges = new GexfEdgeSet();
        }

        public override XElement Render(GexfXml xml, GexfGraph graph)
        {
            XElement element = base.Render(xml, graph);

            if (Nodes.Any())
            {
                element.Add(Nodes.ToXml(xml, graph));
            }

            if (Edges.Any())
            {
                element.Add(Edges.ToXml(xml, graph));
            }
           
            return element;
        }
    }
}