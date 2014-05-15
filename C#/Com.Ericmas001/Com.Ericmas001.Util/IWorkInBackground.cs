using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Util
{
    public interface IWorkInBackground
    {
        string Summary { get; }
        void Work(BackgroundWorker worker, DoWorkEventArgs e);
    }
}
