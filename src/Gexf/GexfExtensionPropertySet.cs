using System.Xml.Linq;

namespace Gexf
{
    public class GexfExtensionPropertySet : GexfHashSet<IGexfExtensionProperty>
    {
        public void WriteTo(GexfXml xml, XElement element)
        {
            foreach (IGexfExtensionProperty property in this)
            {
                property.WriteTo(xml, element);
            }
        }
    }
}