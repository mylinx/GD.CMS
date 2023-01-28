using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GD.CMS.Common 
{
    public enum UserEnum
    {

        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = -1,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1
    }
}
