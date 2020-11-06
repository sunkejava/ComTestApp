using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTestApp.Common
{
    public class StaticCommon
    {
        private static bool _IsStop = true;

        /// <summary>
        /// 单开线程读写
        /// </summary>
        /// <returns></returns>
        public static bool ThIsStop
        {
            get {

                lock (new object())
                {
                    return _IsStop;
                }
            }
            set 
            {
                lock (new object())
                {
                    _IsStop = value;
                }
            }
            
        }
        /// <summary>
        /// 主线程读写
        /// </summary>
        public static bool IsStop
        {
            get { return _IsStop; }
            set { _IsStop = value; }
        }

    }
}
