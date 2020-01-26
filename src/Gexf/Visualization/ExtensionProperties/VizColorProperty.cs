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

        public void WriteTo(GexfOutput output, XElement element)
        {
            element.Add(output.Viz.Element("color",
                output.Attribute("r", _color.R),
                output.Attribute("g", _color.G),
                output.Attribute("b", _color.B)
            ));
        }
    }
}