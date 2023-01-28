using System;
using System.Collections.Generic;
using System.Text;

namespace GD.CMS.Common
{

    /// <summary>
    /// 返回集合列表
    /// </summary>
   public  class LayResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public long Count { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
