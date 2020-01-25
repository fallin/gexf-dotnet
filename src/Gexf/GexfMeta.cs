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

        public XElement ToXml(GexfOutput output)
        {
            bool AnyMetadataSpecified() => LastModified.HasValue
                || !string.IsNullOrEmpty(Creator)
                || !string.IsNullOrEmpty(Description)
                || Keywords.Any();

            var element = output.When(AnyMetadataSpecified,
                () =>
                    output.Gexf.Element("meta",

                        output.When(() => LastModified.HasValue,
                            () => output.Attribute("lastmodifieddate", LastModified)),

                        output.When(() => !string.IsNullOrEmpty(Creator),
                            () => output.Gexf.Element("creator", Creator)
                        ),

                        output.When(() => !string.IsNullOrEmpty(Description),
                            () => output.Gexf.Element("description", Description)
                        ),

                        output.When(() => Keywords.Any(),
                            () => output.Gexf.Element("keywords", string.Join(",", Keywords)
                            )
                        )
                ));
            return element;
        }
    }
}