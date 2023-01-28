using System;
using System.Collections.Generic;
using System.Text;
using FreeSql;
using FreeSql.DataAnnotations;

namespace GD.CMS.Entity
{
    /// <summary>
    /// 权限表
    /// </summary>
    [Table(Name = "gd_permision")]
    public class PermissionEntity : BaseEntity
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }


        /// <summary>
        /// 父节点
        /// </summary>
        public int PID
        {
            get;
            set;
        }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PName
        {
            get;
            set;
        }
        /// <summary>
        /// 路径
        /// </summary>
        public string Paths
        {
            get;
            set;
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icons
        {
            get;
            set;
        }

        /// <summary>
		/// 等级
        /// </summary>
        public int Leves
        {
            get;
            set;
        }

        /// <summary>
		/// 是否禁用（1启用   0 禁用）
        /// </summary>
        public int? IsDisable
        {
            get;
            set;
        }


        /// <summary>
        /// Remark
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        [Navigate("pid")]
        public PermissionEntity PEntity { get; set; }
    }
}
