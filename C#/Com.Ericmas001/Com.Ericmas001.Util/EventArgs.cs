using System;

namespace Com.Ericmas001.Util
{
    class EventArgs<T> : EventArgs
    {
        public T Arg { get; private set; }

        public EventArgs(T arg)
        {
            Arg = arg;
        }
    }
}
