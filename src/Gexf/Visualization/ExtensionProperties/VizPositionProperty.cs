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

        public void WriteTo(GexfXml xml, XElement element)
        {
            element.Add(xml.Viz.Element("position",
                xml.Attribute("x", _position.X),
                xml.Attribute("y", _position.Y),
                xml.Attribute("z", _position.Z)
            ));
        }
    }
}