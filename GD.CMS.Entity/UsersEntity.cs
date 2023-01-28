using System;
using FreeSql;
using FreeSql.DataAnnotations;

namespace GD.CMS.Entity
{
    [Table(Name = "gd_user")]
    public class UsersEntity : BaseEntity
    {
        [Column(IsPrimary = true,IsIdentity =true)]
        public int ID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
         
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
         

        /// <summary>
        /// 记录访问IP
        /// </summary>
        public string IP { get; set; }

        [Navigate("RoleID")]
        public RoleEntity roleEntity { get; set; }
    }
}
