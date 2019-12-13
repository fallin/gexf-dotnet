using System.Linq;

namespace DotNet.Gexf
{
    public sealed class GexfOptions
    {
        private readonly string[] _options;

        public GexfOptions(params string[] options)
        {
            _options = options.Select(x => x.Trim()).ToArray();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join("|", _options);
        }
    }
}