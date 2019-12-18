using System;
using System.Collections.Generic;

namespace DotNet.Gexf
{
    public abstract class GexfHashSet<TObject> : HashSet<TObject> where TObject : IIdentifiable<GexfId> 
    {
        protected GexfHashSet() : base(IdentityComparer)
        {
        }

        protected GexfHashSet(IEnumerable<TObject> collection) : base(collection, IdentityComparer)
        {
        }

        public void AddRange(IEnumerable<TObject> collection)
        {
            foreach (TObject item in collection)
            {
                Add(item);
            }
        }

        public static IEqualityComparer<TObject> IdentityComparer { get; } = new IdentifiableEqualityComparer<TObject, GexfId>();
    }
}