using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.Core.Repositories {
    public class SoftDeleteAttribute : Attribute {
        public SoftDeleteAttribute(string column, string dateColumn) {
            ColumnName = column;
            DateColumnName = dateColumn;
        }

        public string ColumnName { get; set; }
        public string DateColumnName { get; set; }
    }
}
