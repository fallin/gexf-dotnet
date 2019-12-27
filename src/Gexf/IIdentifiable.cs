using System;

namespace DotNet.Gexf
{
    /// <summary>
    /// A uniquely identifiable object (keyed object)
    /// </summary>
    public interface IIdentifiable<out T> where T : IEquatable<T>
    {
        T Id { get; }
    }
}