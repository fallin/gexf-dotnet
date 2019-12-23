using System.Xml.Linq;

namespace DotNet.Gexf
{
    public interface IGexfExtensionProperty : IIdentifiable<GexfId>
    {
        void WriteTo(GexfXml xml, XElement element);
    }
}