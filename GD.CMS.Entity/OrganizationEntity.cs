using System;
using FreeSql;
using FreeSql.DataAnnotations;

namespace GD.CMS.Entity
{
    [Table(Name = "gd_organization")]
    public class OrganizationEntity : BaseEntity
    {
        [Column(IsPrimary = true,IsIdentity =true)]
        public int ID { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string Name { get; set; }
         
        /// <summary>
        /// 上级组织ID，最顶级为-1；
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 上级组织名称，最顶级默认值为空
        /// </summary>
        public string PName { get; set; }

    }
}
