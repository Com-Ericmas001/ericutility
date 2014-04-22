using System;
using System.Threading;

namespace Com.Ericmas001.Util.Concurrency
{
    public class ReaderWriterObjectLocker
    {
        #region BaseReleaser

        private class BaseReleaser
        {
            protected readonly ReaderWriterObjectLocker m_Locker;

            protected BaseReleaser(ReaderWriterObjectLocker locker)
            {
                m_Locker = locker;
            }
        }

        #endregion BaseReleaser

        #region ReaderReleaser

        private class ReaderReleaser : BaseReleaser, IDisposable
        {
            public ReaderReleaser(ReaderWriterObjectLocker locker)
                : base(locker)
            {
            }

            #region IDisposable Members

            public void Dispose()
            {
                m_Locker.m_Locker.ReleaseReaderLock();
            }

            #endregion IDisposable Members
        }

        #endregion ReaderReleaser

        #region WriterReleaser

        private class WriterReleaser : BaseReleaser, IDisposable
        {
            public WriterReleaser(ReaderWriterObjectLocker locker)
                : base(locker)
            {
            }

            #region IDisposable Members

            public void Dispose()
            {
                m_Locker.m_Locker.ReleaseWriterLock();
            }

            #endregion IDisposable Members
        }

        #endregion WriterReleaser

        #region Fields

        private readonly ReaderWriterLock m_Locker;
        private readonly IDisposable m_WriterReleaser;
        private readonly IDisposable m_ReaderReleaser;

        #endregion Fields

        #region Constructor

        public ReaderWriterObjectLocker()
        {
            // TODO: update to ReaderWriterLockSlim on .net 3.5
            m_Locker = new ReaderWriterLock();

            m_WriterReleaser = new WriterReleaser(this);
            m_ReaderReleaser = new ReaderReleaser(this);
        }

        #endregion Constructor

        #region Methods

        public IDisposable LockForRead()
        {
            m_Locker.AcquireReaderLock(-1);

            return m_ReaderReleaser;
        }

        public IDisposable LockForWrite()
        {
            m_Locker.AcquireWriterLock(-1);

            return m_WriterReleaser;
        }

        #endregion Methods
    }
}