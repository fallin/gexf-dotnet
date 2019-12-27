using System.Collections.Generic;
using System.Linq;

namespace Gexf
{
    public abstract class GexfHashSet<T> : HashSet<T> where T : IIdentifiable<GexfId> 
    {
        protected GexfHashSet() : base(IdentityComparer)
        {
        }

        protected GexfHashSet(IEnumerable<T> collection) : base(collection, IdentityComparer)
        {
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                foreach (T item in collection.Where(x => x != null))
                {
                    Add(item);
                }
            }
        }

        public void AddRange(params T[] collection)
        {
            AddRange((IEnumerable<T>)collection);
        }

        public static IEqualityComparer<T> IdentityComparer { get; } = new IdentifiableEqualityComparer<T, GexfId>();
    }
}