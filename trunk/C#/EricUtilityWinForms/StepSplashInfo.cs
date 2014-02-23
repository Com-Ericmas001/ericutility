using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EricUtility.Windows.Forms
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
