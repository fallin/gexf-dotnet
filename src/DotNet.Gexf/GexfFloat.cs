using System;

namespace DotNet.Gexf
{
    public struct GexfFloat : IEquatable<GexfFloat>
    {
        public const float FloatEpsilon = 0.000001f;

        private readonly float _value;

        public GexfFloat(float value)
        {
            _value = value;
        }

        public bool Equals(GexfFloat other)
        {
            return Math.Abs(_value - other._value) < FloatEpsilon;
        }

        public override bool Equals(object obj)
        {
            return obj is GexfFloat other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(GexfFloat left, GexfFloat right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GexfFloat left, GexfFloat right)
        {
            return !left.Equals(right);
        }

        public static implicit operator GexfFloat(float value)
        {
            return new GexfFloat(value);
        }

        public static implicit operator float(GexfFloat value)
        {
            return value._value;
        }
    }
}