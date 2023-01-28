using Microsoft.AspNetCore.Http;

namespace GD.CMS.Common
{
    public static class HttpContextCore
    {
        public static HttpContext Current { get => AutofacHelper.GetService<IHttpContextAccessor>().HttpContext; }
    }
}
