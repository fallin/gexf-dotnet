using System.Xml.Linq;

namespace Gexf
{
    public class GexfExtensionPropertySet : GexfHashSet<IGexfExtensionProperty>
    {
        public void WriteTo(GexfOutput output, XElement element)
        {
            foreach (IGexfExtensionProperty property in this)
            {
                property.WriteTo(output, element);
            }
        }
    }
}