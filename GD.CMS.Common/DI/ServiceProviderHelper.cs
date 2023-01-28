using System;
using System.Collections.Generic;
using System.Text;

namespace GD.CMS.Common
{

   /// <summary>
   /// 属性注入
   /// </summary>
   public static class ServiceProviderHelper
    {
        public static IServiceProvider ServiceProvider
        {
            get; set;
        }
    }
}
