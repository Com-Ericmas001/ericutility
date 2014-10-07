using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace Com.Ericmas001.Util
{
    /// <summary>
    /// Custom Dynamic Object for dealing with DataRow Objects
    /// </summary>
    public sealed class DynamicRow : DynamicObject
    {
        private readonly DataRow _row;

        public DynamicRow(DataRow row)
        {
            _row = row;
        }

        public DataRow DataRow
        {
            get { return _row; }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var retVal = _row.Table.Columns.Contains(binder.Name);
            result = retVal ? _row[binder.Name] : null;
            return retVal;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var retVal = _row.Table.Columns.Contains(binder.Name);
            if (retVal)
                _row[binder.Name] = value;
            return retVal;
        }

        public static IEnumerable<DynamicRow> Convert(DataTable table)
        {
            return from DataRow row in table.Rows select new DynamicRow(row);
        }
    }
}
