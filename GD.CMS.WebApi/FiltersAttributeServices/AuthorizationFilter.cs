using GD.CMS.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GD.CMS.Service;
using GD.CMS.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace GD.CMS.WebApi
{

    /// <summary>
    ///  授权特性
    /// </summary>
    public class AuthorizationFilter : Attribute, IAsyncActionFilter
    {
        private readonly IJwtService _jwtService;
        public AuthorizationFilter() 
        {
            _jwtService = ServiceProviderHelper.ServiceProvider.GetRequiredService<IJwtService>();
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            var result = new AjaxResult
            {
                ErrorCode = 401,
                Msg = "未登录",
                Success=false
            };

            if (token.IsNullOrEmpty())
            { 
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    ContentType = "application/json;charset=utf-8",
                    Content = result.ToJson()
                };

                context.Result.ExecuteResultAsync(context);
            }


            return next();
        }

    }
}
