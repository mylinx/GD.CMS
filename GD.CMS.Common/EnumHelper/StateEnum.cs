using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GD.CMS.Common
{

    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum ArticleStateEnum
    {
        ///// <summary>
        ///// 未删除
        ///// </summary>
        //[Description("未删除")]
        //Del = -1, 

        /// <summary>
        /// 保存
        /// </summary>
        [Description("保存")]
         Save=0,

        /// <summary>
        /// 已发布
        /// </summary>
        [Description("发布")]
        Published = 1
    }

    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum IshowEnum
    {
        /// <summary>
        /// 不显示
        /// </summary>
        [Description("不显示")]
        unshow = 0,

        /// <summary>
        /// 显示
        /// </summary>
        [Description("显示")]
        show = 1
    }
}
