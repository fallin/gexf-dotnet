using System;

namespace DotNet.Gexf
{
    public interface IExtensionPropertyContainer
    {
        GexfExtensionPropertySet ExtensionProperties { get; }
    }
}