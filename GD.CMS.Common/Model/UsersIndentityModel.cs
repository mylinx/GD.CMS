using System;
using System.Collections.Generic;
using System.Text;

namespace GD.CMS.Common
{
  public  class UsersIndentityModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }


        /// <summary>
        /// 角色
        /// </summary>
        public int RoleID { get; set; }

        public string Roles { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }

    }
}
