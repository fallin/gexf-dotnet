using System.Xml.Linq;

namespace DotNet.Gexf.Hierarchy
{
    public class GexfParentedNode : GexfNode
    {
        public GexfId Parent { get; }

        public GexfParentedNode(GexfId id) : this(id, string.Empty)
        {
        }

        public GexfParentedNode(GexfId id, string label) : this(id, label, default)
        { }

        public GexfParentedNode(GexfId id, string label, GexfId parent)
            : base(id, label)
        {
            Parent = parent;
        }

        public override XElement ToXml(GexfXml xml, GexfGraph graph)
        {
            XElement element = base.ToXml(xml, graph);

            if (Parent != null)
            {
                element.Add(xml.Attribute("pid", Parent));
            }

            return element;
        }
    }
}