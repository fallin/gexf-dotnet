using System;
using System.Xml.Linq;

namespace Gexf
{
    public class GexfOutput
    {
        private readonly GexfOutputSettings _settings;

        public GexfElementFactory Gexf { get; }
        public GexfElementFactory Viz { get; }

        public GexfOutput() : this(new GexfOutputSettings())
        { }

        public GexfOutput(GexfOutputSettings settings)
        {
            _settings = settings;
            Gexf = new GexfElementFactory("http://www.gexf.net/1.2draft");
            Viz = new GexfElementFactory("http://www.gexf.net/1.2draft/viz");
        }

        /// <summary>
        /// Allows conditional content for use in XElement and XAttribute constructors
        /// </summary>
        /// <typeparam name="T">A LINQ-to-XML type</typeparam>
        /// <param name="condition">A predicate function to determine whether to return conditional content</param>
        /// <param name="content">A function to which returns the conditional content</param>
        /// <returns>Returns the conditional content, when the condition function is true; otherwise, null</returns>
        /// <remarks>
        /// This technique works because the XElement and XAttribute types ignore null values that
        /// are passed into the content (or content[]). This function takes advantage of this slightly
        /// odd quirk to provide conditional content in the XElement and XAttribute constructors.
        /// </remarks>
        public T When<T>(Func<bool> condition, Func<T> content) where T : XObject
        {
            return condition() ? content() : null;
        }

        public XAttribute Attribute(string name, GexfId id)
        {
            return Attribute(name, id.Match<object>(i => i, s => s));
        }

        public XAttribute Attribute(string name, DateTimeOffset value)
        {
            return Attribute(name, value.ToString("yyyy-MM-dd"));
        }

        public XAttribute Attribute(string name, DateTimeOffset? value)
        {
            return value.HasValue ? Attribute(name, value.Value) : null;
        }

        public XAttribute DefaultedAttribute<T>(string name, T? value, T defaultValue) where T : struct, Enum
        {
            switch (_settings.DefaultValueHandling)
            {
                case GexfDefaultValueHandling.IncludeIfAssigned:
                    return When(() => value.HasValue, () => Attribute(name, value));
                case GexfDefaultValueHandling.Include:
                    return Attribute(name, value ?? defaultValue);
                case GexfDefaultValueHandling.Ignore:
                    value = value ?? defaultValue;
                    return When(() => !value.Equals(defaultValue), () => Attribute(name, value));
                default:
                    throw new ArgumentOutOfRangeException(nameof(_settings.DefaultValueHandling),
                        _settings.DefaultValueHandling, "Unsupported GEXF DefaultValueHandling value specified");
            }
        }

        public XAttribute Attribute(string name, object value)
        {
            if (value is Enum @enum)
            {
                value = @enum.ToString("g").ToLowerInvariant();
            }

            return new XAttribute(name, value);
        }

        public class GexfElementFactory
        {
            internal GexfElementFactory(string namespaceUrl)
            {
                Namespace = namespaceUrl;
            }

            public XNamespace Namespace { get; }

            public XElement Element(string name, object content)
            {
                return new XElement(Namespace + name, content);
            }

            public XElement Element(string name, params object[] content)
            {
                return new XElement(Namespace + name, content);
            }
        }
    }
}