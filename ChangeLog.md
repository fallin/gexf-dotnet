# gexf-dotnet change log

0.2.0

- Refines behavior around default attribute values. Previously, if an attribute value matched the GEXF-defined default value, it would not be included in the output GEXF document. When Saving the document, the caller can now specify the preferred default value handling: IncludeIfAssigned, Include, or Ignore. The default is now IncludeIfAssigned, so (for example) assigning `gexf.Graph.IdType = GexfIdType.Integer;` (which is defined by GEXF as the default id-type for a graph) will result in the idtype attribute being written to the output document, even though it matches the default IdType. This seems like it provides more intuitive behavior. See GexfDefaultValueHandling for additional options.

0.1.1

- Fixes bug with incorrectly serializing vs:size value

0.1.0

- Initial release