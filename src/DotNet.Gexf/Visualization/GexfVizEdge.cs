using System.Drawing;
using System.Xml.Linq;

namespace DotNet.Gexf.Visualization
{
    public class GexfVizEdge : GexfEdge
    {
        public Color? Color { get; set; }
        public GexfFloat Thickness { get; set; }
        public GexfEdgeShape Shape { get; set; }

        public GexfVizEdge(GexfId id, GexfId source, GexfId target) : base(id, source, target)
        {
            Thickness = 1.0f;
            Shape = GexfEdgeShape.Solid;
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

            if (!Thickness.Equals(1.0f))
            {
                element.Add(xml.Viz.Element("thickness",
                    xml.Attribute("value", Thickness)
                ));
            }

            if (Shape != GexfEdgeShape.Solid)
            {
                element.Add(xml.Viz.Element("shape",
                    xml.Attribute("value", Shape)
                ));
            }

            return element;
        }
    }
}