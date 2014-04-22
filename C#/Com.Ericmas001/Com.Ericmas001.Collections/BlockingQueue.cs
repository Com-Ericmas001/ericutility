using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Com.Ericmas001.Collections
{
    public class BlockingQueue<T> : IEnumerable<T>
    {
        private int m_Count;
        private readonly Queue<T> m_Queue = new Queue<T>();

        public T Dequeue()
        {
            lock (m_Queue)
            {
                while (m_Count <= 0) Monitor.Wait(m_Queue);
                m_Count--;
                return m_Queue.Dequeue();
            }
        }

        public void Enqueue(T data)
        {
            if (data == null) throw new ArgumentNullException("data");
            lock (m_Queue)
            {
                m_Queue.Enqueue(data);
                m_Count++;
                Monitor.Pulse(m_Queue);
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            while (true) yield return Dequeue();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            while (true) yield return Dequeue();
        }
    }
}