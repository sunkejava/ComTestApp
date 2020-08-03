using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTestApp.Entitys
{
    [CommonEnitityMapping(TableName = "AllIn_PortInfo")]
    public class UsbPortEntity
    {
        [CommonEnitityMapping(ColumnName = "ID")]
        public int Id { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [CommonEnitityMapping(ColumnName = "Num", Notes = "序号",VisibleForDv = true)]
        public int Num { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        [CommonEnitityMapping(ColumnName = "BatchNumber", Notes = "批次号", VisibleForDv = true)]
        public string BatchNumber { get; set; }
        /// <summary>
        /// 串口名称
        /// </summary>
        [CommonEnitityMapping(ColumnName = "SerialPortName", Notes = "串口名称", VisibleForDv = true)]
        public string SerialPortName { get; set; }
        /// <summary>
        /// 箱体ID
        /// </summary>
        [CommonEnitityMapping(ColumnName = "BoxId", Notes = "箱体编号", VisibleForDv = true)]
        public string BoxId { get; set; }
        /// <summary>
        /// 板卡ID
        /// </summary>
        [CommonEnitityMapping(ColumnName = "CardId", Notes = "板卡编号", VisibleForDv = true)]
        public string CardId { get; set; }
        /// <summary>
        /// 板卡名称
        /// </summary>
        [CommonEnitityMapping(ColumnName = "CardName", Notes = "板卡名称", VisibleForDv = true)]
        public string CardName { get; set; }
        /// <summary>
        /// 端口ID
        /// </summary>
        [CommonEnitityMapping(ColumnName = "PortId", Notes = "端口编号", VisibleForDv = true)]
        public string PortId { get; set; }
        /// <summary>
        /// 端口状态
        /// </summary>
        [CommonEnitityMapping(ColumnName = "PortStatus", Notes = "检测结果", VisibleForDv = true)]
        public string PortStatus { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [CommonEnitityMapping(ColumnName = "StartTime", Notes = "开始时间", VisibleForDv = true,IsDate = true)]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [CommonEnitityMapping(ColumnName = "EndTime", Notes = "结束时间", VisibleForDv = true, IsDate = true)]
        public DateTime EndTime { get; set; } 
        /// <summary>
        /// 设备类型
        /// </summary>
        [CommonEnitityMapping(ColumnName = "hardType",Notes = "设备类型")]
        public string HardType { get; set; }
    }
}
