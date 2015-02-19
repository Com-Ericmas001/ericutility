using Com.Ericmas001.Wpf.ViewModels;

namespace Com.Ericmas001.AppMonitor.DataTypes.DataElements
{
    public abstract class BaseDataElement : BaseViewModel
    {
        public abstract string Header { get; }
    }
}
