using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GD.CMS.Common
{
   public enum DisableEnum
    {
        /// <summary>
        /// 隐藏
        /// </summary>
        [Description("隐藏")]
        Disable = 0,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        UnDisable = 1
    }
}
