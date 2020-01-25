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

        public void WriteTo(GexfOutput output, XElement element)
        {
            element.Add(output.Viz.Element("shape",
                output.Attribute("value", _shape),
                output.When(() => _shape == GexfNodeShape.Image,
                    () => output.Attribute("uri", ImageUrl))
            ));
        }
    }
}