using System.Xml.Linq;

namespace Gexf
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

        public XDocument ToXml(GexfOutputSettings settings)
        {
            GexfOutput output = new GexfOutput(settings);

            XDocument document = new XDocument(
                new XDeclaration("1.1", "utf-8", "yes"),
                output.Gexf.Element("gexf",
                    new XAttribute("xmlns", output.Gexf.Namespace),
                    new XAttribute(XNamespace.Xmlns + "viz", output.Viz.Namespace),
                    new XAttribute("version", "1.2"),
                    Meta.ToXml(output),
                    Graph.ToXml(output)
                    )
            );

            return document;
        }

        public void Save(string fileName, GexfOutputSettings settings = null)
        {
            settings = settings ?? new GexfOutputSettings();

            var document = ToXml(settings);
            document.Save(fileName, SaveOptions.OmitDuplicateNamespaces);
        }
    }
}