using System.Xml.Linq;

namespace Gexf
{
    public interface IGexfExtensionProperty : IIdentifiable<GexfId>
    {
        void WriteTo(GexfXml xml, XElement element);
    }
}