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

        public void WriteTo(GexfXml xml, XElement element)
        {
            if (_parent != null)
            {
                element.Add(xml.Attribute("pid", _parent));
            }
        }
    }
}