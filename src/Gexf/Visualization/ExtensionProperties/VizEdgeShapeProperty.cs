using System.Xml.Linq;

namespace Gexf.Visualization.ExtensionProperties
{
    public class VizEdgeShapeProperty : IGexfExtensionProperty
    {
        private readonly GexfEdgeShape _shape;

        public GexfId Id { get; } = "viz:shape";

        public VizEdgeShapeProperty(GexfEdgeShape shape)
        {
            _shape = shape;
        }

        public void WriteTo(GexfOutput output, XElement element)
        {
            element.Add(output.Viz.Element("shape",
                output.Attribute("value", _shape)
            ));
        }
    }
}