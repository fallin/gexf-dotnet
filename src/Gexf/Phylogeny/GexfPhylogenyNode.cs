using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Gexf.Phylogeny
{
    public class GexfPhylogenyNode : GexfNode
    {
        public List<GexfId> Parents { get; }

        public GexfPhylogenyNode(GexfId id)
            : this(id, string.Empty)
        {
        }

        public GexfPhylogenyNode(GexfId id, string label)
            : base(id, label)
        {
            Parents = new List<GexfId>();
        }

        public override XElement ToXml(GexfXml xml, GexfGraph graph)
        {
            var element = base.ToXml(xml, graph);

            if (Parents.Any())
            {
                XElement parentsElement = xml.Gexf.Element("parents");

                foreach (GexfId parent in Parents)
                {
                    parentsElement.Add(xml.Gexf.Element("parent",
                        xml.Attribute("for", parent)));
                }

                element.Add(parentsElement);
            }

            return element;
        }
    }
}