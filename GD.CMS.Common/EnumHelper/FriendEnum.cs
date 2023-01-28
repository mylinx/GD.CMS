using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GD.CMS.Common
{

    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum Leves
    {
        /// <summary>
        /// 省份
        /// </summary>
        [Description("省份")]
        pv = 1,

        /// <summary>
        /// 市
        /// </summary>
        [Description("市")]
         ct=2,

        /// <summary>
        /// 县（区）
        /// </summary>
        [Description("县/区")]
        cr =3
    }
}
