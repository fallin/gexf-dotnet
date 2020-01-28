using System.Xml.Linq;

namespace Gexf
{
    public sealed class GexfAttribute : IIdentifiable<GexfId>
    {
        public GexfId Id { get; }
        public string Title { get; set; }
        public GexfDataType Type { get; set; }

        public object Default { get; set; }
        public GexfOptions Options { get; set; }

        public GexfAttribute(GexfId id, string title, GexfDataType type) : this(id)
        {
            Title = title;
            Type = type;
        }

        public GexfAttribute(GexfId id)
        {
            Id = id;
        }

        public XElement ToXml(GexfOutput output)
        {
            var element = output.Gexf.Element("attribute",
                output.Attribute("id", Id),
                output.Attribute("title", Title),
                output.Attribute("type", Type)
            );

            if (Default != null)
            {
                element.Add(output.Gexf.Element("default"), Default);
            }

            if (Options != null)
            {
                element.Add(output.Gexf.Element("options", Options));
            }

            return element;
        }
    }
}