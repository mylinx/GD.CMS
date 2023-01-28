using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.CMS.Entity.Dto.OutputDto.Users
{
    /// <summary>
    /// 列表
    /// </summary>
    public class ListDto
    {
        public int ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public int RoleName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public int CreateTime { get; set; }

        public string IP { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        public int Status { get; set; }

        public int Sort { get; set; }
    }
}
