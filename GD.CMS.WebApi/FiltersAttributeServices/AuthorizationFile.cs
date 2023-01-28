using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.CMS.WebApi.FiltersAttributeServices
{
    public class AuthorizationFile : IAuthorizationFilter,IActionFilter
    {
        /// <summary>
        /// 3.最后执行这里
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }


        /// <summary>
        /// 2.再行这里
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
             
        }


        /// <summary>
        /// 1.先执行这里
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //string authorization= context.HttpContext.Request.Headers["Authorization"];
            //
        }
    }
}
