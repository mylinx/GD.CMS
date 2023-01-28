using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GD.CMS.Common
{

    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum LogTypeEnum
    {
        /// <summary>
        /// 消息
        /// </summary>
        [Description("消息")]
        Info = 1,


        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        Erro = 2
    } 
}
