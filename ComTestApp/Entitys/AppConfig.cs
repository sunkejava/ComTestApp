using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTestApp.Entitys
{
    [CommonEnitityMapping(TableName = "Sys_Config")]
    public class AppConfig
    {
        [CommonEnitityMapping(ColumnName = "ID")]
        public int Id { get; set; } = 1;
        [CommonEnitityMapping(ColumnName = "comStr")]
        public int comIndex { get; set; }
        [CommonEnitityMapping(ColumnName = "hardType")]
        public int hardTypeIndex { get; set; }
        [CommonEnitityMapping(ColumnName = "hardNum")]
        public decimal hardNum { get; set; }
        [CommonEnitityMapping(ColumnName = "checkNum")]
        public int checkNumIndex { get; set; }
        [CommonEnitityMapping(ColumnName = "checkModel")]
        public int checkModelIndex { get; set; }
        [CommonEnitityMapping(ColumnName = "waitNum")]
        public decimal waitNum { get; set; }
    }
}
