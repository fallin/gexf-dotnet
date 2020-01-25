using System.Xml.Linq;

namespace Gexf.Visualization.ExtensionProperties
{
    public class VizThicknessProperty : IGexfExtensionProperty
    {
        private readonly GexfFloat _thickness;

        public GexfId Id { get; } = "viz:thickness";

        public VizThicknessProperty(GexfFloat thickness)
        {
            _thickness = thickness;
        }

        public void WriteTo(GexfOutput output, XElement element)
        {
            element.Add(output.Viz.Element("thickness",
                output.Attribute("value", _thickness)
            ));
        }
    }
}