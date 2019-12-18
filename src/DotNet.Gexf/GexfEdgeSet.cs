using System.Collections.Generic;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfEdgeSet : GexfHashSet<GexfEdge>
    {
        public GexfEdgeSet()
        {
        }

        public GexfEdgeSet(IEnumerable<GexfEdge> collection) : base(collection)
        {
        }

        public XElement Render(GexfXml xml, GexfGraph graph)
        {
            XElement edges = xml.Gexf.Element("edges",
                xml.Attribute("count", this.Count)
                );

            foreach (GexfEdge edge in this)
            {
                edges.Add(edge.Render(xml, graph));
            }

            return edges;
        }
    }
}