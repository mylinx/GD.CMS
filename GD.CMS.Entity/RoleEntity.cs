using FreeSql;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GD.CMS.Entity
{
    [Table(Name = "gd_role")]
    public class RoleEntity : BaseEntity
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string Pid { get; set; } 

    }
}
