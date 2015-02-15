using System.ComponentModel;

namespace Com.Ericmas001.Util
{
    public interface IWorkInBackground
    {
        string Summary { get; }
        void Work(BackgroundWorker worker, DoWorkEventArgs e);
    }
}
