using System;
using System.Diagnostics;

namespace Com.Ericmas001.Util
{
    /// <summary>
    /// MyStopwatch is a debbuging tool to count the amout of time used by an operation.
    /// </summary>
    public class MyStopwatch : IDisposable
    {
        #region Fields

        private readonly Stopwatch m_InternalStopwatch;
        private readonly string m_Name;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes the MyStopwatch
        /// </summary>
        /// <param name="name">The name of MyStopwatch</param>
        public MyStopwatch(string name)
        {
#if DEBUG
            m_Name = name;
            m_InternalStopwatch = new Stopwatch();
            m_InternalStopwatch.Start();
#endif
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Disposes the MyStopwatch and writes into debug the amount of time used on the operation.
        /// </summary>
        public void Dispose()
        {
#if DEBUG
            m_InternalStopwatch.Stop();
            Debug.WriteLine(m_Name + ": " + m_InternalStopwatch.Elapsed);
#endif
        }

        #endregion Methods
    }
}