using System;

namespace Gexf
{
    /// <summary>
    /// A uniquely identifiable object (keyed object)
    /// </summary>
    public interface IIdentifiable<out T> where T : IEquatable<T>
    {
        T Id { get; }
    }
}