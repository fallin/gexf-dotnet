using System.Xml.Linq;

namespace Gexf.Visualization.ExtensionProperties
{
    public class VizSizeProperty : IGexfExtensionProperty
    {
        private readonly GexfFloat _size;

        public GexfId Id { get; } = "viz:size";

        public VizSizeProperty(GexfFloat size)
        {
            _size = size;
        }

        public void WriteTo(GexfXml xml, XElement element)
        {
            element.Add(xml.Viz.Element("size",
                xml.Attribute("value", _size)
            ));
        }
    }
}