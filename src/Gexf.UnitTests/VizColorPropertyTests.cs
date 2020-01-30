using System.Drawing;
using System.Xml.Linq;
using FluentAssertions;
using Gexf.Visualization.ExtensionProperties;
using NUnit.Framework;

namespace Gexf.UnitTests
{
    [TestFixture]
    public class VizColorPropertyTests
    {
        [Test]
        public void WriteToShouldOutputRgbWhenProvidedColorWithoutAlphaChannel()
        {
            Color expectedColor = Color.Coral;
            var property = new VizColorProperty(expectedColor);

            GexfOutput output =  new GexfOutput();
            XElement element = new XElement("node");
            property.WriteTo(output, element);

            var viz = output.Viz.Namespace;
            element.Should()
                .HaveElement(viz + "color").Which.Should()
                .HaveAttribute("r", $"{expectedColor.R}").And
                .HaveAttribute("g", $"{expectedColor.G}").And
                .HaveAttribute("b", $"{expectedColor.B}");

            element.Attribute("a").Should().BeNull("the alpha channel was not specified");
        }

        [Test]
        public void WriteToShouldOutputRgbaWhenProvidedColorWithAlphaChannel()
        {
            Color expectedColor = Color.FromArgb(20, Color.Coral);
            var property = new VizColorProperty(expectedColor);

            GexfOutput output = new GexfOutput();
            XElement element = new XElement("node");
            property.WriteTo(output, element);

            var viz = output.Viz.Namespace;
            element.Should()
                .HaveElement(viz + "color").Which.Should()
                .HaveAttribute("r", $"{expectedColor.R}").And
                .HaveAttribute("g", $"{expectedColor.G}").And
                .HaveAttribute("b", $"{expectedColor.B}").And
                .HaveAttribute("a", "20");
        }
    }
}
