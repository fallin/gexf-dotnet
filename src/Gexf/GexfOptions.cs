using System;
using System.Collections.Generic;
using System.Linq;

namespace Gexf
{
    public sealed class GexfOptions : HashSet<string>
    {
        public GexfOptions(params string[] options) : base(ValidOptions(options))
        {
        }

        public void AddRange(IEnumerable<string> values)
        {
            foreach (string value in ValidOptions(values))
            {
                Add(value);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join("|", this);
        }

        public static GexfOptions Parse(string text)
        {
            text = text ?? string.Empty;

            string[] options = text.Split(new[] {'|', ',', ';'}, StringSplitOptions.RemoveEmptyEntries);
            return new GexfOptions(options);
        }

        static IEnumerable<string> ValidOptions(IEnumerable<string> options)
        {
            return options.Where(i => !string.IsNullOrWhiteSpace(i));
        }
    }
}