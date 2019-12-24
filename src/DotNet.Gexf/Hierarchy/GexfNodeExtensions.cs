namespace DotNet.Gexf.Hierarchy
{
    public static class GexfNodeExtensions
    {
        public static GexfNode Parent(this GexfNode node, GexfId parent)
        {
            node.ExtensionProperties.Add(new ParentProperty(parent));
            return node;
        }
    }
}