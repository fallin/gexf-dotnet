using System.Xml.Linq;

namespace Gexf
{
    public sealed class GexfGraph
    {
        public GexfNodeSet Nodes { get; }
        public GexfEdgeSet Edges { get; }

        public GexfModeType? Mode { get; set; }
        public GexfEdgeType? DefaultedEdgeType { get; set; }
        public GexfIdType? IdType { get; set; }

        public GexfAttributeSet NodeAttributes { get; }
        public GexfAttributeSet EdgeAttributes { get; }

        public GexfGraph()
        {
            Nodes = new GexfNodeSet();
            Edges = new GexfEdgeSet();

            NodeAttributes = new GexfAttributeSet(GexfClassType.Node);
            EdgeAttributes = new GexfAttributeSet(GexfClassType.Edge);
        }

        public XElement ToXml(GexfOutput output)
        {
            var element = output.Gexf.Element("graph",

                output.DefaultedAttribute("mode", Mode, GexfModeType.Static),
                output.DefaultedAttribute("defaultedgetype", DefaultedEdgeType, GexfEdgeType.Undirected),
                output.DefaultedAttribute("idtype", IdType, GexfIdType.String),

                NodeAttributes.ToXml(output),
                EdgeAttributes.ToXml(output),
                Nodes.ToXml(output, this),
                Edges.ToXml(output, this)
            );
            return element;
        }
    }
}