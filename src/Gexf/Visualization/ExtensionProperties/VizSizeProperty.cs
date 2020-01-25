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

        public void WriteTo(GexfOutput output, XElement element)
        {
            element.Add(output.Viz.Element("size",
                output.Attribute("value", _size)
            ));
        }
    }
}