using GD.CMS.Entity.Dto;
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
    public class UsersListParams: PageParams
    {  
        /// <summary>
       /// 开始时间
       /// </summary>
        public DateTime? StartTime { get; set; } = null;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; } = null;


        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }


        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

    }
}
