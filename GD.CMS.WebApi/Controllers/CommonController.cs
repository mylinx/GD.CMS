using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GD.CMS.Common;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using GD.CMS.Entity;
using GD.CMS.Service;
using Microsoft.AspNetCore.Hosting;

namespace GD.CMS.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonController : ControllerBase
    {

        readonly IWebHostEnvironment webHostEnvironment;
        public CommonController()
        {
        }


        /// <summary>
        /// 获取code值
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetCode(string code)
        {
            return (string)CacheHelper.SystemCache.GetCache(code);
        }

        /// <summary>
        /// 获取vcode
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public string GetVCode() 
        {
            string code ="";//生成字符
            var  ms= VerificatCodeHelper.Create(out code);
            //_cache.SetCache(code, code);
            CacheHelper.SystemCache.SetCache(code, code);
            var data = new 
            {
                 code,
                 img= Convert.ToBase64String(ms.ToArray())
            };
            return data.ToJson();
        }
    }
}
