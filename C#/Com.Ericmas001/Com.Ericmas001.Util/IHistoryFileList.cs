using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
