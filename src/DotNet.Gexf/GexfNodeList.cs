using System.Collections.Generic;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfNodeList : HashSet<GexfNode>
    {
        public XElement Render(GexfXml xml, GexfGraph graph)
        {
            XElement nodes = xml.Gexf.Element("nodes",
                xml.Attribute("count", this.Count)
                );

            foreach (GexfNode node in this)
            {
                nodes.Add(node.Render(xml, graph));
            }

            return nodes;
        }
    }
}