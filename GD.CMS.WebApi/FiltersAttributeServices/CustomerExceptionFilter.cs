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
    //public class AuthorizationFile : Attribute, IAuthorizationFilter,IActionFilter,IExceptionFilter
    public class CustomerExceptionFilter : IActionFilter, IExceptionFilter
    {
        private readonly ILogService _logService;
        public CustomerExceptionFilter() 
        {
            _logService = ServiceProviderHelper.ServiceProvider.GetRequiredService<ILogService>();
        }

        /// <summary>
        /// 3.最后执行这里
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            _logService.Insert(new LogsEntity()
            {
                Logcontent = $"执行路由{controller}/{action}",
                Logtype = "信息",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            });
        }


        /// <summary>
        /// 2.再行这里
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }



        public async void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                // 定义返回数据 
                var result = new AjaxResult
                {
                    ErrorCode = 500,
                    Msg = "内部出错"
                };

                context.Result = new ContentResult
                {
                    StatusCode = 500,
                    ContentType = "application/json;charset=utf-8",
                    Content = result.ToJson()
                };

                await _logService.InsertAsync(new LogsEntity()
                {
                     Logcontent=context.Exception.Message.ToString(),
                      Logtype="异常",
                       CreateTime=DateTime.Now,
                        UpdateTime=DateTime.Now
                });
            }
            context.ExceptionHandled = true;
        }
    }
}
