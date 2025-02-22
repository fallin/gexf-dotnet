using System.Drawing;
using System.Xml.Linq;
using Gexf.Visualization.ExtensionProperties;
using NUnit.Framework;
using Shouldly;

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

            var colorElement = ShouldHaveElement(element, viz + "color");
            ShouldHaveAttribute(colorElement, "r", $"{expectedColor.R}");
            ShouldHaveAttribute(colorElement, "g", $"{expectedColor.G}");
            ShouldHaveAttribute(colorElement, "b", $"{expectedColor.B}");
            
            colorElement.Attribute("a").ShouldBeNull("the alpha channel was not specified");
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
            
            var colorElement = ShouldHaveElement(element, viz + "color");
            ShouldHaveAttribute(colorElement, "r", $"{expectedColor.R}");
            ShouldHaveAttribute(colorElement, "g", $"{expectedColor.G}");
            ShouldHaveAttribute(colorElement, "b", $"{expectedColor.B}");
            ShouldHaveAttribute(colorElement, "a", "20");
        }

        static XElement ShouldHaveElement(XElement element, XName name)
        {
            var child = element.Element(name);
            child.ShouldNotBeNull($"Expected element {element.Name} to contain child element {name}");
            return child;
        }

        static void ShouldHaveAttribute(XElement element, XName name, string value)
        {
            element.Attribute(name).ShouldSatisfyAllConditions(
                a => a.ShouldNotBeNull($"Expected element {element.Name} to contain attribute {name}"),
                a => a.Value.ShouldBe(value)
                );
        }
    }
}
