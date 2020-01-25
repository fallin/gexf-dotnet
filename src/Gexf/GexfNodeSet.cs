using System.Collections.Generic;
using System.Xml.Linq;

namespace Gexf
{
    public sealed class GexfNodeSet : GexfHashSet<GexfNode>
    {
        public GexfNodeSet()
        {
        }

        public GexfNodeSet(IEnumerable<GexfNode> collection) : base(collection)
        {
        }

        public XElement ToXml(GexfOutput output, GexfGraph graph)
        {
            XElement nodes = output.Gexf.Element("nodes",
                output.Attribute("count", this.Count)
                );

            foreach (GexfNode node in this)
            {
                nodes.Add(node.ToXml(output, graph));
            }

            return nodes;
        }
    }
}