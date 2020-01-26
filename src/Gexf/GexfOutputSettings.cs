namespace Gexf
{
    public class GexfOutputSettings
    {
        /// <summary>
        /// Specifies the desired behavior around writing default values
        /// </summary>
        /// <remarks>
        /// GEXF specifies default values for certain attributes. For example, the
        /// default value for the GEXF graph's ID type is string. If the Graph.IdType
        /// is set to a value other than the default, it just be written to the GEXF
        /// document. This setting determines the desired behavior when the value
        /// matches the default.
        /// </remarks>
        public GexfDefaultValueHandling DefaultValueHandling { get; set; }

        public GexfOutputSettings()
        {
            DefaultValueHandling = GexfDefaultValueHandling.IncludeIfAssigned;
        }
    }
}