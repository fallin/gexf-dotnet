using System;
using System.Drawing;
using System.Numerics;
using DotNet.Gexf.Visualization.ExtensionProperties;

namespace DotNet.Gexf.Visualization
{
    public static class GexfNodeExtensions
    {
        public static VizColorProperty Color(this GexfNode node, Color color)
        {
            var property = new VizColorProperty(color);
            node.ExtensionProperties.Add(property);
            return property;
        }

        public static VizPositionProperty Position(this GexfNode node, Vector3 position)
        {
            var property = new VizPositionProperty(position);
            node.ExtensionProperties.Add(property);
            return property;
        }

        public static VizSizeProperty Size(this GexfNode node, GexfFloat size)
        {
            var property = new VizSizeProperty(size);
            node.ExtensionProperties.Add(property);
            return property;
        }

        public static VizNodeShapeProperty Shape(this GexfNode node, GexfNodeShape shape)
        {
            var property = new VizNodeShapeProperty(shape);
            node.ExtensionProperties.Add(property);
            return property;
        }

        public static VizNodeShapeProperty Image(this GexfNode node, Uri imageUrl)
        {
            var property = new VizNodeShapeProperty(GexfNodeShape.Image)
            {
                ImageUrl = imageUrl
            };
            node.ExtensionProperties.Add(property);
            return property;
        }
    }
}