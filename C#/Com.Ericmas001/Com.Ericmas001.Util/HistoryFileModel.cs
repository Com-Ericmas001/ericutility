using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Util
{
    public class HistoryFileModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public int MaxHistory { get; set; }
        public string FileName { get; private set; }
        public ObservableCollection<string> Items { get; private set; }

        public HistoryFileModel(string filename)
        {
            MaxHistory = 25;
            Items = new ObservableCollection<string>();
            FileName = filename;
            try
            {
                if (File.Exists(filename))
                    File.ReadAllLines(filename).ToList().ForEach(it => Items.Add(it));
            }
            catch
            {
                Items = new ObservableCollection<string>();
            }
        }

        public string MostRecent
        {
            get { return Count > 0 ? Items[0] : ""; }
        }

        public int Count
        {
            get { return Math.Min(Items.Count, MaxHistory); }
        }

        public void AddEntry(string entry)
        {
            if (Items.Contains(entry))
                Items.Move(Items.IndexOf(entry), 0);
            else
                Items.Insert(0, entry);
            SaveHistory();
            RaisePropertyChanged("Items");
        }

        public void SaveHistory()
        {
            try
            {
                StreamWriter sw = new StreamWriter(FileName);
                for (int i = 0; i < Count; ++i)
                    sw.WriteLine(Items[i]);
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
