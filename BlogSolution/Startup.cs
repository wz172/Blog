using Blog.IResponse;
using Blog.IService;
using Blog.Response;
using Blog.Service;
using BlogModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BlogSolution
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ע��JWT��֤
           services.AddJwtService(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogSolution", Version = "v1" });

                #region Awagger ʹ����Ȩ���

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name= "Authorization",
                    Description = "ֱ����������������Bearer {token}",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme{
                            Reference=new  OpenApiReference{
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }

                        },new string[]{ }
                    }
                });

                #endregion
            });

            //ע��sqlsugar����  Ҫ���� SqlSugar.IOC
            services.AddSqlSugar(new SqlSugar.IOC.IocConfig()
            {
                ConnectionString = Configuration.GetConnectionString("sqlServer"),
                DbType = SqlSugar.IOC.IocDbType.SqlServer,
                IsAutoCloseConnection = true
            });

            //ע���Լ���д���� ��չ����
            services.AddMySelfService();

            //ע��automapper���� ɨ��Profile �ļ� �д��Ż�
            services.AddAutoMapper(System.Reflection.Assembly.Load("Blog.Mapping"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogSolution v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //����˭
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ExpIServiceCollection
    {
        public static IServiceCollection AddMySelfService(this IServiceCollection service)
        {
            service.AddScoped<IBaseResponse<Author>, BlogAuthorResponse>();
            service.AddScoped<IBaseResponse<BlogNews>, BlogBlogNewsResponse>();
            service.AddScoped<IBaseResponse<BlogTypeInfo>, BlogTypeInfoResponse>();

            service.AddScoped<IAuthorService, BlogAuthorService>();
            service.AddScoped<IBlogNewsServer, BlogNewsService>();
            service.AddScoped<IBlogTypeInfoService, BlogTypeInfoService>();
            return service;
        }

        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
              options =>
              {
                  var configKeyBytes = System.Text.Encoding.UTF8.GetBytes(Configuration["SignatureKey:loginKey"]);
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidIssuer = Configuration["SignatureKey:Issuer"],

                      ValidateAudience = true,
                      ValidAudience = Configuration["SignatureKey:Audience"],

                      ValidateLifetime = true,

                      IssuerSigningKey = new SymmetricSecurityKey(configKeyBytes)

                  };
              }
              );
            return services;
        }
    }
}
