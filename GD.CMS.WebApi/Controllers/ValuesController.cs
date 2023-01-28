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

namespace GD.CMS.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IJwtService _jwtService;
        IBaseService<LogsEntity> _baseService;
        IBaseService<RoleEntity> _rolesService;
        public ValuesController(IJwtService jwtService,
            IBaseService<LogsEntity> baseService, 
            IBaseService<RoleEntity> rolesService)
        {
            _jwtService = jwtService;
            _baseService = baseService;
            _rolesService = rolesService;
        }

        [Authorize(Roles = "admin")]
        public string AuthorizeTest()
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            return "Say Hello!";
        }



        [Authorize(Roles = "admin,vip")]
        public string AuthorizeTestTow()
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            return "Say Hello!";
        }


        /// <summary>
        /// 获取token (1.比如增加文件上传的时候怎么处理？)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public string GetToken1([FromBody] UsersEntity entity)
        {
            return  entity.Account +  "   密码："+entity.Password;
        }


            /// <summary>
            /// 获取token
            /// </summary>
            /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public string GetToken(string uname,string pwd) 
        {
            if (uname == "admin" && pwd == "123456")
            {
                var tt = _jwtService.GetToken(new UsersIndentityModel()
                {
                    Account = "admin",
                    ID = "1",
                    IsAdmin = false,
                    RoleID =1,
                    RoleName = "admin"
                });

                return tt.ToJson();
            }
            else if (uname == "user" && pwd == "123456")
            {
                var tt = _jwtService.GetToken(new UsersIndentityModel()
                {
                    Account = "vip",
                    ID = "1",
                    IsAdmin = false,
                    RoleID=2,
                    RoleName = "vip"
                });
                return tt.ToJson();
            }
            else {
                return "游客登录！";
                //var tt = _jwtService.GetToken(new UsersIndentityModel()
                //{
                //    Account = "user",
                //    ID = "1",
                //    IsAdmin = false,
                //    Roles = "user"
                //});
                //return tt.ToJson();
            }
        }


        /// <summary>
        /// 解析Token
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public string AnalyseToken() 
        {
            //var list= _baseService.Select.ToList();
            //var roles = _rolesService.Select.ToList();
            return "这是公共方法";
        }
    }
}
