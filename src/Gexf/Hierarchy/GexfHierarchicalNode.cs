using System.Linq;
using System.Xml.Linq;

namespace Gexf.Hierarchy
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

        public override XElement ToXml(GexfOutput output, GexfGraph graph)
        {
            XElement element = base.ToXml(output, graph);

            if (Nodes.Any())
            {
                element.Add(Nodes.ToXml(output, graph));
            }

            if (Edges.Any())
            {
                element.Add(Edges.ToXml(output, graph));
            }
           
            return element;
        }
    }
}