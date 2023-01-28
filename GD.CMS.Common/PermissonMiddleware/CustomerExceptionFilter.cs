using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GD.CMS.Common;
using Microsoft.AspNetCore.Mvc;
namespace GD.CMS.Common
{
    public class CustomerExceptionFilter : IAsyncExceptionFilter
    { 

        public Task OnExceptionAsync(ExceptionContext context)
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
                    StatusCode =500,
                    ContentType = "application/json;charset=utf-8",
                    Content = result.ToJson()
                };
            }
            context.ExceptionHandled = true;
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
