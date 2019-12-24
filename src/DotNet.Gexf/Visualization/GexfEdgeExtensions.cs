using System;
using System.Drawing;
using DotNet.Gexf.Visualization.ExtensionProperties;

namespace DotNet.Gexf.Visualization
{
    public static class GexfEdgeExtensions
    {
        public static VizColorProperty Color(this GexfEdge edge, Color color)
        {
            var property = new VizColorProperty(color);
            edge.ExtensionProperties.Add(property);
            return property;
        }

       public static VizThicknessProperty Thickness(this GexfEdge edge, GexfFloat thickness)
        {
            var property = new VizThicknessProperty(thickness);
            edge.ExtensionProperties.Add(property);
            return property;
        }

        public static VizEdgeShapeProperty Shape(this GexfEdge edge, GexfEdgeShape shape)
        {
            var property = new VizEdgeShapeProperty(shape);
            edge.ExtensionProperties.Add(property);
            return property;
        }
    }
}