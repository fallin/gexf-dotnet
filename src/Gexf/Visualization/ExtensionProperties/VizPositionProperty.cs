using System.Numerics;
using System.Xml.Linq;

namespace Gexf.Visualization.ExtensionProperties
{
    public class VizPositionProperty : IGexfExtensionProperty
    {
        private readonly Vector3 _position;

        public GexfId Id { get; } = "viz:position";

        public VizPositionProperty(Vector3 position)
        {
            _position = position;
        }

        public void WriteTo(GexfOutput output, XElement element)
        {
            element.Add(output.Viz.Element("position",
                output.Attribute("x", _position.X),
                output.Attribute("y", _position.Y),
                output.Attribute("z", _position.Z)
            ));
        }
    }
}