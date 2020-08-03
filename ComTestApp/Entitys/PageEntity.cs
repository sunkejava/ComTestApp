using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTestApp.Entitys
{
    public class PageEntity
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public string PageCount { get; set; }
        /// <summary>
        /// 每页N条
        /// </summary>
        public string PageSize { get; set; }

    }
}
