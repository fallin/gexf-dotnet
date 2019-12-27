using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Gexf
{
    public sealed class GexfMeta
    {
        public DateTimeOffset? LastModified { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public HashSet<string> Keywords { get; }

        public GexfMeta()
        {
            Keywords = new HashSet<string>();
        }

        public void UpdateLastModified()
        {
            LastModified = DateTimeOffset.Now;
        }

        public XElement ToXml(GexfXml xml)
        {
            bool AnyMetadataSpecified() => Specified(LastModified)
                || Specified(Creator)
                || Specified(Description)
                || Keywords.Any();

            var element = xml.When(AnyMetadataSpecified,
                () =>
                    xml.Gexf.Element("meta",

                        xml.When(() => Specified(LastModified),
                            () => xml.Attribute("lastmodifieddate", LastModified)),

                        xml.When(() => Specified(Creator),
                            () => xml.Gexf.Element("creator", Creator)
                        ),

                        xml.When(() => Specified(Description),
                            () => xml.Gexf.Element("description", Description)
                        ),

                        xml.When(() => Keywords.Any(),
                            () => xml.Gexf.Element("keywords", string.Join(",", Keywords)
                            )
                        )
                ));
            return element;
        }

        static bool Specified<T>(T? nullable) where T : struct => nullable.HasValue;
        static bool Specified(string value) => !string.IsNullOrEmpty(value);
    }
}