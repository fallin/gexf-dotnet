using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfEdgeList : Collection<GexfEdge>
    {
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