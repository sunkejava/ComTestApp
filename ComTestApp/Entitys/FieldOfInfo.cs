using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTestApp.Entitys
{
    /// <summary>
    /// 字段信息
    /// </summary>
    [Serializable]
    public class FieldOfInfo
    {
        /// <summary>
        /// Entity字段名称
        /// </summary>
        public string EntityName { get; set; } = "";

        /// <summary>
        /// 表字段名称
        /// </summary>
        public string ColumnName { get; set; } = "";

        /// <summary>
        /// 注释
        /// </summary>
        public string Notes { get; set; } = "";

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; } = false;

        /// <summary>
        /// 是否日期类型
        /// </summary>
        public bool IsDate { get; set; } = false;
        /// <summary>
        /// 是否数字类型
        /// </summary>
        public bool IsNum { get; set; } = false;
        /// <summary>
        /// 选择类型(枚举类型,例如：AllIn.Common.ConstValue+WorkCategory,AllIn.Common)
        /// </summary>
        public string SelectType { get; set; } = "";

        /// <summary>
        /// 是否在DataGridView中显示,默认不显示
        /// </summary>
        public bool VisibleForDv { get; set; } = false;

        /// <summary>
        /// 是否为选择框
        /// </summary>
        public bool IsCheckBox { get; set; } = false;

    }
}
