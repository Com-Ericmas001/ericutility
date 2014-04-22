using System;

namespace Com.Ericmas001.Windows.Forms
{
    public abstract class StepSplashInfo
    {
        public delegate bool BoolEmptyHandler();
        public abstract string Title { get; }
        public abstract Tuple<BoolEmptyHandler, string>[] Steps { get; }

        public virtual void Init()
        {
        }
    }
}
