using System;
using System.Drawing;
using DotNet.Gexf.Visualization.ExtensionProperties;

namespace DotNet.Gexf.Visualization
{
    public static class GexfEdgeExtensions
    {
        public static GexfEdge Color(this GexfEdge edge, Color color)
        {
            edge.ExtensionProperties.Add(new VizColorProperty(color));
            return edge;
        }

       public static GexfEdge Thickness(this GexfEdge edge, GexfFloat thickness)
        {
            edge.ExtensionProperties.Add(new VizThicknessProperty(thickness));
            return edge;
        }

        public static GexfEdge Shape(this GexfEdge edge, GexfEdgeShape shape)
        {
            edge.ExtensionProperties.Add(new VizEdgeShapeProperty(shape));
            return edge;
        }
    }
}