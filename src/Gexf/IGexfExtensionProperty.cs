using System.Xml.Linq;

namespace Gexf
{
    public interface IGexfExtensionProperty : IIdentifiable<GexfId>
    {
        void WriteTo(GexfOutput output, XElement element);
    }
}