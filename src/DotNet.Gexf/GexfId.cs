using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotNet.Gexf
{
    public abstract class GexfId : IEquatable<GexfId>
    {
        public GexfIdType Type { get; }

        protected GexfId(GexfIdType type)
        {
            Type = type;
        }

        public abstract T Match<T>(Func<int, T> intFunc, Func<string, T> strFunc);

        [DebuggerDisplay("Id={" + nameof(_id) + "}")]
        public class Int : GexfId
        {
            readonly int _id;

            public Int(int id) : base(GexfIdType.Integer)
            {
                _id = id;
            }

            public override T Match<T>(Func<int, T> intFunc, Func<string, T> strFunc)
            {
                return intFunc(_id);
            }

            public override string ToString()
            {
                return _id.ToString();
            }
        }

        [DebuggerDisplay("Id={" + nameof(_id) + "}")]
        public class Str : GexfId 
        {
            readonly string _id;

            public Str(string id) : base(GexfIdType.String)
            {
                _id = id;
            }

            public override T Match<T>(Func<int, T> intFunc, Func<string, T> strFunc)
            {
                return strFunc(_id);
            }

            public override string ToString()
            {
                return _id;
            }
        }

        // public void Match(Action<int> intId, Action<string> strId)
        // {
        //     // Adapt leaf action to func (ret null)
        //     object AdaptInt(int value)
        //     {
        //         intId?.Invoke(value);
        //         return null;
        //     }
        //
        //     // Adapt node action to func (ret null)
        //     object AdaptStr(string value)
        //     {
        //         strId?.Invoke(value);
        //         return null;
        //     }
        //
        //     Match(AdaptInt, AdaptStr);
        // }

        public bool Equals(GexfId other)
        {
            if (other == null) return false;

            return Match(
                intLeft =>
                {
                    return other.Match(intRight => intLeft == intRight, _ => false);
                },
                strLeft =>
                {
                    return other.Match(_ => false, strRight => strLeft == strRight);
                }
            );
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as GexfId;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return Match(i => i.GetHashCode(), s => s.GetHashCode());
        }

        public static bool operator ==(GexfId left, GexfId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GexfId left, GexfId right)
        {
            return !Equals(left, right);
        }

        public static implicit operator GexfId(int id)
        {
            return new Int(id);
        }

        public static implicit operator GexfId(string id)
        {
            return new Str(id);
        }
    }
}