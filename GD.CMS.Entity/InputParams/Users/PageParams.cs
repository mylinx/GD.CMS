using System;
using System.Collections.Generic;
using System.Text;


/**
* 版权: Copyright(c) 2021
* 作者：mylinx 
* 时间：2021-4-30 12:59:26
* 功能说明：
*
**/
namespace GD.CMS.Entity 
{
    public class PageParams
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
