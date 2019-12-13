using System;
using System.Diagnostics;

namespace DotNet.Gexf
{
    [DebuggerDisplay("Id={Id}")]
    public sealed class GexfId : IEquatable<GexfId>
    {
        public object Id { get; }
        public GexfIdType Type { get; }

        public GexfId(int id) : this(id, GexfIdType.Integer)
        {
        }

        public GexfId(string id) : this(id, GexfIdType.String)
        {
        }

        private GexfId(object id, GexfIdType type)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Type = type;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Id.ToString();
        }

        /// <inheritdoc />
        public bool Equals(GexfId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is GexfId other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Id != null ? Id.GetHashCode() : 0;
        }

        public static bool operator ==(GexfId left, GexfId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GexfId left, GexfId right)
        {
            return !Equals(left, right);
        }

        public static implicit operator GexfId(int i)
        {
            return new GexfId(i);
        }

        public static implicit operator GexfId(string s)
        {
            return new GexfId(s);
        }
    }
}