using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GD.CMS.Common
{

    /// <summary>
    /// 删除
    /// </summary>
    public enum DelEnum
    {

        /// <summary>
        /// 未删除
        /// </summary>
        [Description("未删除")]
         Undel=0,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Del = 1
    }
}
