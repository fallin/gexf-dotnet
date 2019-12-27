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

        public void WriteTo(GexfXml xml, XElement element)
        {
            element.Add(xml.Viz.Element("thickness",
                xml.Attribute("value", _thickness)
            ));
        }
    }
}