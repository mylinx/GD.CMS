using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GD.CMS.Entity;
using GD.CMS.Common;
namespace GD.CMS.Service
{
    public interface  ILoginService
    {

        /// <summary>
        /// 账号登录
        /// </summary>
        /// <returns></returns>
        public Task<AjaxResult> LoginByAccount(LoginDto dto);



    }
}
