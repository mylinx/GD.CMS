using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.CMS.Entity
{
    public class LoginDto
    {
         public string Account { get; set; } //账号

        public string Password { get; set; } //密码

        public string Vcode { get; set; }   //验证码
    }
}
