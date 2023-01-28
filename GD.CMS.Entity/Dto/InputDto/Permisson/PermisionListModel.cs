using System;
using System.Collections.Generic;
using System.Text;


/**
* 版权: Copyright(c) 2021
* 作者：mylinx 
* 时间：
* 功能说明：
*
**/
namespace GD.CMS.Entity
{
    public class PermisionListModel
    {
        public int id { get; set; }

        public int pid { get; set; }
        public object pname { get; set; }
        public string title { get; set; }


        /// <summary>
        /// 标题
        /// </summary>
        public string icons { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int? isdisable { get; set; }
        public string paths { get; set; }

        public int sort { get; set; }
    }
}
