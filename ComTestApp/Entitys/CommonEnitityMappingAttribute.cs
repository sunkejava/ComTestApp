using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTestApp.Entitys
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = true)]
    public class CommonEnitityMappingAttribute : BaseEntityHelper.EnitityMappingAttribute
    {
        /// <summary>
        /// 注释，默认为""
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 是否必填，默认否
        /// </summary>
        public bool IsRequired { get; set; } = false;

        /// <summary>
        /// 是否日期类型,默认否
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
        /// 是否选择框
        /// </summary>
        public bool IsCheckBox { get; set; } = false;

        /// <summary>
        /// 是否在DataGridView中显示,默认不显示
        /// </summary>
        public bool VisibleForDv { get; set; } = false;
    }
}
