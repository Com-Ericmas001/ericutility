using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Collections
{
    public class ConcurrentList<T> : ConcurrentDictionary<T, T>
    {
        public ConcurrentList()
            : base()
        {

        }
        public ConcurrentList(ConcurrentList<T> list)
            : base()
        {
        }

        public List<T> ToList()
        {
            return Keys.ToList();
        }

        public void Add(T item)
        {
            base.AddOrUpdate(item, item, delegate { return item; });
        }

        public bool Contains(T item)
        {
            return base.ContainsKey(item);
        }

        public void Remove(T item)
        {
            T removed;
            base.TryRemove(item, out removed);
        }
    }
}
