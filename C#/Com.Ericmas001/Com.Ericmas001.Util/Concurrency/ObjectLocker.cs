using System;
using System.Threading;

namespace Com.Ericmas001.Util.Concurrency
{
    public class ObjectLocker : IDisposable
    {
        #region Fields

        private readonly object m_Obj;

        #endregion Fields

        #region Constructor

        public ObjectLocker(object obj)
        {
            m_Obj = obj;
            Monitor.Enter(m_Obj);
        }

        #endregion Constructor

        #region IDisposable Members

        public void Dispose()
        {
            Monitor.Exit(m_Obj);
        }

        #endregion IDisposable Members
    }
}