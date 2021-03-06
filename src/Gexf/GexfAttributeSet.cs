﻿using System.Linq;
using System.Xml.Linq;

namespace Gexf
{
    public sealed class GexfAttributeSet : GexfHashSet<GexfAttribute>
    {
        public GexfClassType Class { get; }

        public GexfAttributeSet(GexfClassType @class)
        {
            Class = @class;
        }

        public XElement ToXml(GexfOutput output)
        {
            XElement element = null;

            if (this.Any())
            {
                element = output.Gexf.Element("attributes",
                    output.Attribute("class", Class)
                );

                foreach (GexfAttribute attribute in this)
                {
                    element.Add(attribute.ToXml(output));
                }
            }

            return element;
        }
    }
}