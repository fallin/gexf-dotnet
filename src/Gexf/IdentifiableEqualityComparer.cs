using System;
using System.Collections.Generic;

namespace Gexf
{
    public sealed class IdentifiableEqualityComparer<TObject, TKey> : IEqualityComparer<TObject>
        where TObject : IIdentifiable<TKey> 
        where TKey : IEquatable<TKey>
    {
        public bool Equals(TObject x, TObject y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(TObject obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}