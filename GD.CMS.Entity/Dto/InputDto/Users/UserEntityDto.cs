using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.CMS.Entity.Dto
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserEntityDto
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
    }
}
