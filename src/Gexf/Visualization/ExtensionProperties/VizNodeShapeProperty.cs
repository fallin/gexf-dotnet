using System;
using System.Xml.Linq;

namespace Gexf.Visualization.ExtensionProperties
{
    public class VizNodeShapeProperty : IGexfExtensionProperty
    {
        private readonly GexfNodeShape _shape;

        public GexfId Id { get; } = "viz:shape";

        public VizNodeShapeProperty(GexfNodeShape shape)
        {
            _shape = shape;
        }

        public Uri ImageUrl { get; set; }

        public void WriteTo(GexfXml xml, XElement element)
        {
            element.Add(xml.Viz.Element("shape",
                xml.Attribute("value", _shape),
                xml.When(() => _shape == GexfNodeShape.Image,
                    () => xml.Attribute("uri", ImageUrl))
            ));
        }
    }
}