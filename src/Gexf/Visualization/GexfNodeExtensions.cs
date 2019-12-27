using System;
using System.Drawing;
using System.Numerics;
using Gexf.Visualization.ExtensionProperties;

namespace Gexf.Visualization
{
    public static class GexfNodeExtensions
    {
        public static GexfNode Color(this GexfNode node, Color color)
        {
            node.ExtensionProperties.Add(new VizColorProperty(color));
            return node;
        }

        public static GexfNode Position(this GexfNode node, Vector3 position)
        {
            node.ExtensionProperties.Add(new VizPositionProperty(position));
            return node;
        }

        public static GexfNode Size(this GexfNode node, GexfFloat size)
        {
            node.ExtensionProperties.Add(new VizSizeProperty(size));
            return node;
        }

        public static GexfNode Shape(this GexfNode node, GexfNodeShape shape)
        {
            node.ExtensionProperties.Add(new VizNodeShapeProperty(shape));
            return node;
        }

        public static GexfNode Image(this GexfNode node, Uri imageUrl)
        {
            node.ExtensionProperties.Add(new VizNodeShapeProperty(GexfNodeShape.Image)
            {
                ImageUrl = imageUrl
            });
            return node;
        }
    }
}