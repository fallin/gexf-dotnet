using System;

namespace Gexf
{
    public struct GexfFloat : IEquatable<GexfFloat>
    {
        /// <summary>
        /// 9 digits of precision should be adequate
        /// https://www.explainxkcd.com/wiki/index.php/2170:_Coordinate_Precision
        /// </summary>
        public const double Tolerance = 0.000000001f;

        private readonly double _value;

        public GexfFloat(double value)
        {
            _value = value;
        }

       public bool Equals(GexfFloat other)
        {
            return Math.Abs(_value - other._value) < Tolerance;
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

        public static implicit operator GexfFloat(double value)
        {
            return new GexfFloat(value);
        }

        public static implicit operator double(GexfFloat value)
        {
            return value._value;
        }

        public override string ToString()
        {
            // This is an odd format specifier, but it provides a minimum of
            // 1 digit of precision, but will include additional precision
            // up to 15 digits which is about all double-precision can represent
            // https://tinyurl.com/floating-point-numeric-types

            // The custom numeric format string uses section separators ("pos;neg;zero").
            // When the negative section is missing, the positive formatter applies to the
            // positive and negative values.
            return $"{_value:0.0###############;;0}";
        }
    }
}