using System.Collections;
using System.Collections.Generic;
using Gexf.Visualization.ExtensionProperties;
using NUnit.Framework;

namespace Gexf.UnitTests.Visualization.ExtensionProperties
{
    [TestFixture]
    public class VizSizePropertyTests
    {
        [Test]
        [TestCase(0, ExpectedResult = "0")]
        [TestCase(1.0, ExpectedResult = "1.0")]
        [TestCase(1.999, ExpectedResult = "1.999")]
        [TestCase(2.0375757, ExpectedResult = "2.0375757")]
        [TestCase(-3.0, ExpectedResult = "-3.0")]
        public string WriteToShouldWriteDoubleValue(double size)
        {
            var property = new VizSizeProperty(size);
            var xml = new GexfOutput();
            var nodeElement = xml.Gexf.Element("node");

            property.WriteTo(xml, nodeElement);

            var sizeElement = nodeElement.Element(xml.Viz.Namespace + "size");

            return sizeElement?.Attribute("value")?.Value ?? string.Empty;
        }
    }
}
