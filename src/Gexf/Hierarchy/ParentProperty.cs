using System.Xml.Linq;

namespace Gexf.Hierarchy
{
    public class ParentProperty : IGexfExtensionProperty
    {
        private readonly GexfId _parent;

        public GexfId Id { get; } = "pid";

        public ParentProperty(GexfId parent)
        {
            _parent = parent;
        }

        public void WriteTo(GexfOutput output, XElement element)
        {
            if (_parent != null)
            {
                element.Add(output.Attribute("pid", _parent));
            }
        }
    }
}