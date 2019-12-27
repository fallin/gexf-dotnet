using System.Drawing;
using System.Xml.Linq;

namespace Gexf.Visualization.ExtensionProperties
{
    public class VizColorProperty : IGexfExtensionProperty
    {
        private readonly Color _color;

        public GexfId Id { get; } = "viz:color";

        public VizColorProperty(Color color)
        {
            _color = color;
        }

        public void WriteTo(GexfXml xml, XElement element)
        {
            element.Add(xml.Viz.Element("color",
                xml.Attribute("r", _color.R),
                xml.Attribute("g", _color.G),
                xml.Attribute("b", _color.B)
            ));
        }
    }
}