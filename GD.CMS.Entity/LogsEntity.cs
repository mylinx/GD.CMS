using System;
using System.Collections.Generic;
using System.Text;
using FreeSql;
using FreeSql.DataAnnotations;

namespace GD.CMS.Entity
{
    [Table(Name = "gd_logs")]
    public  class LogsEntity:BaseEntity
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 日志类型（信息、警告）
        /// </summary>
        public string Logtype { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Logcontent { get; set; }
         
    }
}
