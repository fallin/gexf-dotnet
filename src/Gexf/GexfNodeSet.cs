using System.Collections.Generic;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfNodeSet : GexfHashSet<GexfNode>
    {
        public GexfNodeSet()
        {
        }

        public GexfNodeSet(IEnumerable<GexfNode> collection) : base(collection)
        {
        }

        public XElement ToXml(GexfXml xml, GexfGraph graph)
        {
            XElement nodes = xml.Gexf.Element("nodes",
                xml.Attribute("count", this.Count)
                );

            foreach (GexfNode node in this)
            {
                nodes.Add(node.ToXml(xml, graph));
            }

            return nodes;
        }
    }
}