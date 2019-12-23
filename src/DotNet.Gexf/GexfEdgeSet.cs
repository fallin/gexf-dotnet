using System.Collections.Generic;
using System.Linq;
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

        public XElement ToXml(GexfXml xml, GexfGraph graph)
        {
            XElement edges = null;

            if (this.Any())
            {
                edges = xml.Gexf.Element("edges",
                    xml.Attribute("count", this.Count)
                );

                foreach (GexfEdge edge in this)
                {
                    edges.Add(edge.Render(xml, graph));
                }
            }

            return edges;
        }
    }
}