using Autofac;
using FreeSql;
using Microsoft.AspNetCore.Http;
namespace GD.CMS.Common
{
    public class AutofacHelper
    {
        public static IContainer Container { get; set; }
        /// <summary>
        /// 获取全局服务
        /// 警告：此方法使用不当会造成内存溢出,一般开发请勿使用此方法,请使用GetScopeService
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetService<T>() where T : class
        { 
            return Container.Resolve<T>();
        }

        /// <summary>
        /// 获取当前请求为生命周期的服务
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetScopeService<T>() where T : class
        {
            //var builder = new ContainerBuilder();
            //builder.RegisterType<T>();
            //Container = builder.Build();

            return (T)GetService<IHttpContextAccessor>().HttpContext.RequestServices.GetService(typeof(T));
        }

        /// <summary>
        /// 获取注入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        //public static T GetAotufacService<T>() where T : class
        //{ 
        //    //实例
        //    var builder = new ContainerBuilder();

        //    //注册
        //    builder.RegisterType<BaseRepository<T,int>>();

        //    //初始化
        //    var  contaner= builder.Build();
            
        //    return contaner.Resolve<T>();
        //}


    }
}  