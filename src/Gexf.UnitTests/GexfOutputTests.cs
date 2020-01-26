using System.Xml.Linq;
using NUnit.Framework;

namespace Gexf.UnitTests
{
    [TestFixture]
    public class GexfOutputTests
    {
        [TestCase(GexfIdType.String, ExpectedResult = "string")]
        [TestCase(GexfIdType.Integer, ExpectedResult = "integer")]
        [TestCase(null, ExpectedResult = null)]
        public string DefaultedAttributeWhenDefaultValueHandlingIsIncludeIfAssignedAndDefaultIsString(GexfIdType? idType)
        {
            var output = new GexfOutput(new GexfOutputSettings
            {
                DefaultValueHandling = GexfDefaultValueHandling.IncludeIfAssigned
            });

            XAttribute attribute = output.DefaultedAttribute("idtype", idType, GexfIdType.String);
            return attribute?.Value;
        }

        [TestCase(GexfIdType.String, ExpectedResult = "string")]
        [TestCase(GexfIdType.Integer, ExpectedResult = "integer")]
        [TestCase(null, ExpectedResult = "string")]
        public string DefaultedAttributeWhenDefaultValueHandlingIsIncludeAndDefaultIsString(GexfIdType? idType)
        {
            var output = new GexfOutput(new GexfOutputSettings
            {
                DefaultValueHandling = GexfDefaultValueHandling.Include
            });

            XAttribute attribute = output.DefaultedAttribute("idtype", idType, GexfIdType.String);
            return attribute?.Value;
        }

        [TestCase(GexfIdType.String, ExpectedResult = null)]
        [TestCase(GexfIdType.Integer, ExpectedResult = "integer")]
        [TestCase(null, ExpectedResult = null)]
        public string DefaultedAttributeWhenDefaultValueHandlingIsIgnoreAndDefaultIsString(GexfIdType? idType)
        {
            var output = new GexfOutput(new GexfOutputSettings
            {
                DefaultValueHandling = GexfDefaultValueHandling.Ignore
            });

            XAttribute attribute = output.DefaultedAttribute("idtype", idType, GexfIdType.String);
            return attribute?.Value;
        }
    }
}
