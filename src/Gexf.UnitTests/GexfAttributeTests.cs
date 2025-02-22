using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using Shouldly;

namespace Gexf.UnitTests
{
    [TestFixture(TestOf = typeof(GexfAttribute))]
    public class GexfAttributeTests
    {
        [Test]
        public void ToXmlShouldRenderMultipleOptionsWithCorrectDelimiter()
        {
            var subject = new GexfAttribute("id", "title", GexfDataType.String)
            {
                Options = new GexfOptions { "value1", "value2" }
            };

            var output = new GexfOutput();
            var document = subject.ToXml(output);

            var writer = new StringWriter();
            document.Save(writer, SaveOptions.OmitDuplicateNamespaces);
            
            writer.ToString().ShouldMatchApproved();
        }
    }
}