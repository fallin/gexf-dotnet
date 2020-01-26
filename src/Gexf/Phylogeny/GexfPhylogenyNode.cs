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

        public override XElement ToXml(GexfOutput output, GexfGraph graph)
        {
            var element = base.ToXml(output, graph);

            if (Parents.Any())
            {
                XElement parentsElement = output.Gexf.Element("parents");

                foreach (GexfId parent in Parents)
                {
                    parentsElement.Add(output.Gexf.Element("parent",
                        output.Attribute("for", parent)));
                }

                element.Add(parentsElement);
            }

            return element;
        }
    }
}