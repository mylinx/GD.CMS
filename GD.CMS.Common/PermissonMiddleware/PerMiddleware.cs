using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GD.CMS.Common;
using Microsoft.AspNetCore.Mvc;

namespace GD.CMS.Common
{

    /// <summary>
    /// 权限中间件
    /// </summary>
   public class PerMiddlewareMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;
        public PerMiddlewareMiddleware(RequestDelegate next, IJwtService jwtService)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            _next = next;
            _jwtService = jwtService;
        }

        public async Task Invoke(HttpContext context)
        {
            string token= context.Request.Headers["Authorization"];
            if (token.IsNullOrEmpty())
            {
                await _next(context);
                return;
            }
            //1.验证是否过期
            var tokenResult = _jwtService.DeclearToken(token);
            if (tokenResult != null && tokenResult.Success)
            {
                await _next(context);
            }
            //2.获取token 进行解析，解析完成之后，进行权限比对
            else
            {
                //1、权限对比


                //2.返回格式
               await context.Response.WriteAsync(tokenResult.ToJson());
            }

        }

    }
}
