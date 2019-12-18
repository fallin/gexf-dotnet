using System;
using System.Drawing;
using System.Numerics;
using System.Xml.Linq;

namespace DotNet.Gexf
{
    public class GexfVizNode : GexfNode
    {
        public Color? Color { get; set; }
        public Vector3? Position { get; set; }
        public GexfFloat Size { get; set; }
        public GexfNodeShape Shape { get; set; }

        public Uri ImageUrl { get; set; }

        /// <inheritdoc />
        public GexfVizNode(GexfId id) : base(id)
        {
            Size = 1.0f;
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

            if (Position.HasValue)
            {
                Vector3 p = Position.Value;
                element.Add(xml.Viz.Element("position",
                    xml.Attribute("x", p.X),
                    xml.Attribute("y", p.Y),
                    xml.Attribute("z", p.Z)
                ));
            }

            if (!Size.Equals(1.0f))
            {
                element.Add(xml.Viz.Element("size",
                    xml.Attribute("value", Size)
                    ));
            }

            if (Shape != GexfNodeShape.Default)
            {
                element.Add(xml.Viz.Element("shape",
                    xml.Attribute("value", Shape),
                    xml.When(() => Shape == GexfNodeShape.Image,
                        () => xml.Attribute("uri", ImageUrl))
                ));
            }

            return element;
        }
    }
}