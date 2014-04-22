using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Com.Ericmas001.Collections
{
    public class ConcurrentList<T> : ConcurrentDictionary<T, T>
    {
        public List<T> ToList()
        {
            return Keys.ToList();
        }

        public new T[] ToArray()
        {
            return Keys.ToArray();
        }

        public void Add(T item)
        {
            AddOrUpdate(item, item, delegate { return item; });
        }

        public bool Contains(T item)
        {
            return ContainsKey(item);
        }

        public void Remove(T item)
        {
            T removed;
            TryRemove(item, out removed);
        }
    }
}
