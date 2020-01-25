namespace Gexf
{
    /// <summary>
    /// Options for default value handling
    /// </summary>
    public enum GexfDefaultValueHandling
    {
        /// <summary>
        /// The value will be included, if assigned, even if it matches the GEXF default value.
        /// </summary>
        IncludeIfAssigned,

        /// <summary>
        /// The value will always be included, even if it matches the GEXF default value.
        /// </summary>
        Include,

        /// <summary>
        /// The value will be ignored (not included) if it matches the GEXF default value.
        /// </summary>
        Ignore
    }
}