using System.Xml.Linq;

namespace DotNet.Gexf
{
    /// <summary>
    /// https://gephi.org/gexf/1.2draft/gexf-12draft-primer.pdf
    /// </summary>
    public sealed class GexfDocument
    {
        public GexfMeta Meta { get; }
        public GexfGraph Graph { get; }

        public GexfDocument()
        {
            Meta = new GexfMeta();
            Graph = new GexfGraph();
        }

        public XDocument Render()
        {
            GexfXml xml = new GexfXml();

            XDocument doc = new XDocument(
                new XDeclaration("1.1", "utf-8", "yes"),
                xml.Gexf.Element("gexf",
                    new XAttribute("xmlns", xml.Gexf.Namespace),
                    new XAttribute(XNamespace.Xmlns + "viz", xml.Viz.Namespace),
                    new XAttribute("version", "1.2"),
                    Meta.Render(xml),
                    Graph.Render(xml)
                    )
            );

            return doc;
        }

        public void Save(string fileName)
        {
            var xdoc = Render();
            xdoc.Save(fileName, SaveOptions.OmitDuplicateNamespaces);
        }
    }
}