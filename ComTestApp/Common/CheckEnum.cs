using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTestApp.Common
{
    public class CheckEnum
    {
        /// <summary>
        /// 检测次数
        /// </summary>
        public enum CheckNum
        {
            /// <summary>
            /// 单次
            /// </summary>
            Single,
            /// <summary>
            /// 连续
            /// </summary>
            Continuity
        }

        /// <summary>
        /// 设备校验模式
        /// </summary>
        public enum DeviceCheckMode
        {
            /// <summary>
            /// U盘驱动验证
            /// </summary>
            Drive,
            /// <summary>
            /// 上海CA证书验证
            /// </summary>
            ShCa
        }

        /// <summary>
        /// 检测模式
        /// </summary>
        public enum CheckModel
        {
            /// <summary>
            /// 顺序
            /// </summary>
            Order,
            /// <summary>
            /// 随机
            /// </summary>
            Random,
            /// <summary>
            /// 冒烟
            /// </summary>
            Bubbling
        }

    }
}
