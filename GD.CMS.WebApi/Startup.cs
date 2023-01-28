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
               .Build();// 2.��ʼ������

            BaseEntity.Initialization(Fsql, null);
        }



        public static IFreeSql Fsql { get; private set; } // 1.����Fsql����
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Fsql);//4.ע�뵥��ģʽ
            services.AddScoped<UnitOfWorkManager>();
            services.AddFreeRepository();//5.�����̳�ӳ  ��ע��

            #region AutoMapper �Զ�ӳ��
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

            //������֤
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(j =>
            {
                j.TokenValidationParameters = new TokenValidationParameters
                {
                    //��֤������
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtBear:Issurer"],

                    //��֤������
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtBear:Audience"],//������

                    
                    //��֤��Կ
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtBear:secretCredentials"])),

                    ValidateLifetime = true, //��֤��������
                    RequireExpirationTime = true, //����ʱ��

                };
            });


            //������·�ɹ���
            //services.AddMvc(option => option.Filters.Add<AuthorizationFilter>());

             //������·�ɹ���
            services.AddMvc(option => option.Filters.Add<CustomerExceptionFilter>());

            //����һ��������Դ���Կ���
            services.AddCors(options =>
            {
                options.AddPolicy("CustomCorsPolicy", policy =>
                {
                    // �趨����������Դ���ж��������','����
                    policy.WithOrigins("http://localhost:9090")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region AutoFac IOC����
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
            //����Ĭ�Ͼ�̬�ļ�
            app.UseStaticFiles();
            //�����Զ��徲̬�ļ�
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "MyStaticUpload")),
                    RequestPath = "/Upload" //��д��һ������·����
            });

            //ע���ȫ�ַ�����������ע�롣
            ServiceProviderHelper.ServiceProvider = app.ApplicationServices;

            app.UseHttpsRedirection();

            app.UseRouting();

            //1.���������֤
            app.UseAuthentication();

            app.UseCors("CustomCorsPolicy");

            //2.������Ȩ��֤
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
