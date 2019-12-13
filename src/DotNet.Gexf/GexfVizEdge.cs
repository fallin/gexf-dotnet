using System.Drawing;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public class GexfVizEdge : GexfEdge
    {
        public Color? Color { get; set; }
        public float Thickness { get; set; }
        public GexfEdgeShape Shape { get; set; }

        /// <inheritdoc />
        public GexfVizEdge(GexfId id) : base(id)
        {
            Thickness = 1.0f;
        }

        /// <inheritdoc />
        public override XElement Render(GexfXml xml, GexfGraph graph)
        {
            XElement element = base.Render(xml, graph);

            if (Color.HasValue)
            {
                Color c = Color.Value;
                element.Add(xml.Viz.Element("color",
                    xml.Attribute("r", c.R),
                    xml.Attribute("g", c.G),
                    xml.Attribute("b", c.B)
                    ));
            }

            if (!GexfFloat.Equal(Thickness, 1.0f))
            {
                element.Add(xml.Viz.Element("thickness",
                    xml.Attribute("value", Thickness)
                ));
            }

            if (Shape != GexfEdgeShape.Default)
            {
                element.Add(xml.Viz.Element("shape",
                    xml.Attribute("value", Shape)
                ));
            }

            return element;
        }
    }
}