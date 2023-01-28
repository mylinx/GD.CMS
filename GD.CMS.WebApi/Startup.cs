using Autofac;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GD.CMS.Common;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace GD.CMS.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Fsql = new FreeSqlBuilder()
               .UseConnectionString(DataType.MySql, Configuration["ConnectionStrings:mysqlCon"])
               .UseAutoSyncStructure(true)
               //.UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
               .Build();// 2.初始化链接

            BaseEntity.Initialization(Fsql, null);
        }



        public static IFreeSql Fsql { get; private set; } // 1.定义Fsql对象
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Fsql);//4.注入单例模式
            services.AddScoped<UnitOfWorkManager>();
            services.AddFreeRepository();//5.基础继承映  射注入

            #region AutoMapper 自动映射
            var serviceAssembly = Assembly.Load("GD.CMS.Service");
            services.AddAutoMapper(serviceAssembly);
            #endregion

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddControllers();
            services.AddRazorPages();
            services.AddSession();
            services.Configure<IISOptions>(options => {
                options.ForwardClientCertificate = false;
            });

            //配置认证
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(j =>
            {
                j.TokenValidationParameters = new TokenValidationParameters
                {
                    //验证发行人
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtBear:Issurer"],

                    //验证受众人
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtBear:Audience"],//受众人

                    
                    //验证密钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtBear:secretCredentials"])),

                    ValidateLifetime = true, //验证生命周期
                    RequireExpirationTime = true, //过期时间

                };
            });


            //这里是路由过滤
            //services.AddMvc(option => option.Filters.Add<AuthorizationFilter>());

             //这里是路由过滤
            services.AddMvc(option => option.Filters.Add<CustomerExceptionFilter>());

            //允许一个或多个来源可以跨域
            services.AddCors(options =>
            {
                options.AddPolicy("CustomCorsPolicy", policy =>
                {
                    // 设定允许跨域的来源，有多个可以用','隔开
                    policy.WithOrigins("http://localhost:9090")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region AutoFac IOC容器
            try
            {
              
                builder.RegisterType<JwtService>().As<IJwtService>();

                #region Service
                var assemblyServices = Assembly.Load("GD.CMS.Service");
                builder.RegisterAssemblyTypes(assemblyServices)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .EnableInterfaceInterceptors();
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.InnerException);
            }
            #endregion
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //设置默认静态文件
            app.UseStaticFiles();
            //设置自定义静态文件
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "MyStaticUpload")),
                    RequestPath = "/Upload" //重写了一个虚拟路径。
            });

            //注册个全局服务，用于属性注入。
            ServiceProviderHelper.ServiceProvider = app.ApplicationServices;

            app.UseHttpsRedirection();

            app.UseRouting();

            //1.开启身份验证
            app.UseAuthentication();

            app.UseCors("CustomCorsPolicy");

            //2.开启授权认证
            app.UseAuthorization();

            app.UseMiddleware<PerMiddlewareMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapAreaControllerRoute(name: "areas", "areas",
                 pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
              });
        }
    }
}
