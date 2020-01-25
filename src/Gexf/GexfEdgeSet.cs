using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Gexf
{
    public sealed class GexfEdgeSet : GexfHashSet<GexfEdge>
    {
        public GexfEdgeSet()
        {
        }

        public GexfEdgeSet(IEnumerable<GexfEdge> collection) : base(collection)
        {
        }

        public XElement ToXml(GexfOutput output, GexfGraph graph)
        {
            XElement edges = null;

            if (this.Any())
            {
                edges = output.Gexf.Element("edges",
                    output.Attribute("count", this.Count)
                );

                foreach (GexfEdge edge in this)
                {
                    edges.Add(edge.Render(output, graph));
                }
            }

            return edges;
        }
    }
}