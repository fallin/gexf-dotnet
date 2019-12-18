using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfGraph
    {
        public GexfNodeSet Nodes { get; }
        public GexfEdgeSet Edges { get; }

        public GexfModeType Mode { get; set; }
        public GexfEdgeType DefaultEdgeType { get; set; }
        public GexfIdType IdType { get; set; }

        public GexfAttributeSet NodeAttributes { get; }
        public GexfAttributeSet EdgeAttributes { get; }

        public GexfGraph()
        {
            Nodes = new GexfNodeSet();
            Edges = new GexfEdgeSet();

            NodeAttributes = new GexfAttributeSet(GexfClassType.Node);
            EdgeAttributes = new GexfAttributeSet(GexfClassType.Edge);

            Mode = GexfModeType.Static;
            DefaultEdgeType = GexfEdgeType.Undirected;
            IdType = GexfIdType.String;
        }

        public XElement Render(GexfXml xml)
        {
            var element = xml.Gexf.Element("graph",

                xml.When(() => Mode != GexfModeType.Static, 
                    () => xml.Attribute("mode", Mode)),

                xml.When(() => DefaultEdgeType != GexfEdgeType.Undirected,
                    () => xml.Attribute("defaultedgetype", DefaultEdgeType)),

                xml.When(() => IdType != GexfIdType.String,
                    () => xml.Attribute("idtype", IdType)),

                NodeAttributes.Render(xml),
                EdgeAttributes.Render(xml),
                Nodes.Render(xml, this),
                Edges.Render(xml, this)
            );
            return element;
        }
    }
}