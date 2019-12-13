using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfGraph
    {
        public GexfNodeList Nodes { get; }
        public GexfEdgeList Edges { get; }

        public GexfModeType Mode { get; set; }
        public GexfEdgeType DefaultEdgeType { get; set; }
        public GexfIdType IdType { get; set; }

        public GexfAttributeList NodeAttributes { get; }
        public GexfAttributeList EdgeAttributes { get; }

        public GexfGraph()
        {
            Nodes = new GexfNodeList();
            Edges = new GexfEdgeList();

            NodeAttributes = new GexfAttributeList(GexfClassType.Node);
            EdgeAttributes = new GexfAttributeList(GexfClassType.Edge);

            Mode = GexfModeType.Default;
            DefaultEdgeType = GexfEdgeType.Default;
            IdType = GexfIdType.Default;
        }

        public XElement Render(GexfXml xml)
        {
            var element = xml.Gexf.Element("graph",
                xml.Attribute("mode", Mode),
                xml.Attribute("defaultedgetype", DefaultEdgeType),

                xml.When(() => IdType != GexfIdType.Default,
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