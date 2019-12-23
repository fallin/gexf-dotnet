using System.Xml.Linq;

namespace DotNet.Gexf.Visualization.ExtensionProperties
{
    public class VizEdgeShapeProperty : IGexfExtensionProperty
    {
        private readonly GexfEdgeShape _shape;

        public GexfId Id { get; } = "viz:shape";

        public VizEdgeShapeProperty(GexfEdgeShape shape)
        {
            _shape = shape;
        }

        public void WriteTo(GexfXml xml, XElement element)
        {
            element.Add(xml.Viz.Element("shape",
                xml.Attribute("value", _shape)
            ));
        }
    }
}