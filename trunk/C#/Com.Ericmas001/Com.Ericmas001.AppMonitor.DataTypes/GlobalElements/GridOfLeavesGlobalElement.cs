using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Input;
using Com.Ericmas001.AppMonitor.DataTypes.TreeElements;
using Com.Ericmas001.Data;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf;

namespace Com.Ericmas001.AppMonitor.DataTypes.GlobalElements
{
    public abstract class GridOfLeavesGlobalElement<TDataItem> : BaseGlobalElement
        where TDataItem : IDataItem
    {
        public GridOfLeavesGlobalElement(BaseBranchTreeElement branch)
            : base(branch)
        {
        }

        public override string Header
        {
            get { return "Data"; }
        }

        public abstract Dictionary<string, Func<TDataItem, object>> Columns { get; }

        private Dictionary<DataRow, BaseLeafTreeElement> m_RowToFeuille;
        private DataRowView m_SelectedRow;

        private BaseLeafTreeElement m_SelectedFeuille;
        public BaseLeafTreeElement SelectedFeuille
        {
            get { return m_SelectedFeuille; }
            set
            {
                m_SelectedFeuille = value;
                RaisePropertyChanged("SelectedFeuille");
            }
        }
        public DataRowView SelectedRow
        {
            get { return m_SelectedRow; }
            set
            {
                m_SelectedRow = value;
                SelectedFeuille = m_RowToFeuille[value.Row];
                RaisePropertyChanged("SelectedRow");
            }
        }

        private RelayCommand m_ExportGridCommand;
        public ICommand ExportGridCommand
        {
            get { return m_ExportGridCommand ?? (m_ExportGridCommand = new RelayCommand(x => ExportToExcel())); }
        }

        public DataTable GridOfLeaves
        {
            get
            {
                List<string> gridColumns = Columns.Keys.ToList();
                DataTable table = new DataTable();
                if(Branch.TreeLeaves.Any())
                {
                    BaseLeafTreeElement feuille = Branch.TreeLeaves.First();
                    gridColumns.ForEach(c => table.Columns.Add(c, Columns[c].Invoke((TDataItem)feuille.DataItem).GetType()));
                }
                else
                    gridColumns.ForEach(c => table.Columns.Add(c));
                m_RowToFeuille = new Dictionary<DataRow, BaseLeafTreeElement>();
                foreach (BaseLeafTreeElement feuille in Branch.TreeLeaves)
                {
                    DataRow row = table.NewRow();
                    foreach (string c in gridColumns)
                    {
                        row[c] = Columns[c].Invoke((TDataItem)feuille.DataItem);
                    }
                    table.Rows.Add(row);
                    m_RowToFeuille.Add(row, feuille);
                }

                return table;
            }
        }

        private void ExportToExcel()
        {
            ExcelExporter ep = new ExcelExporter();
            ep.ExportDataTable(GridOfLeaves);
        }
    }
}
