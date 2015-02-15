using System.Collections.Generic;

namespace Com.Ericmas001.Util
{
    public interface IHistoryFileList
    {
        string MostRecent { get; }
        int Count { get; }
        void AddEntry(string entry);
        IEnumerable<string> AllItems { get; }
    }
}
