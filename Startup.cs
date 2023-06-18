using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PostOpinion.Domain;
using PostOpinion.Interfaces;
using PostOpinion.Repositories;
using PostOpinion.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOpinion
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
            string conString = @"Server=.\SQLEXPRESS;Database=PostOpinionManagment;trusted_Connection=True";
            services.AddDbContext<ApplicationDbContext>(con => con.UseSqlServer(conString));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PostOpinion", Version = "v1" });
            });
            services.AddAutoMapper(typeof(MappingProfile));
            //
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();

            //
            services.AddScoped<CommentRepository>();
            services.AddScoped<PostRepository>();
            services.AddScoped<UserRepository>();

            //services.AddCors(options => options.AddPolicy("AllowAll",
            //    services =>
            //    {
            //        //services.AllowAnyOrigin();
            //        //services.AllowAnyHeader();
            //        //services.AllowAnyMethod();
            //    }));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.WithOrigins("https://localhost:44315")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            ///jwt
            ///
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT: Issuer"],
                    ValidAudience = Configuration["JWT: Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "WebAPI",
                    Description = "Product WebAPI"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                        new OpenApiSecurityScheme{ Reference = new OpenApiReference { Id = "Bearer",Type = ReferenceType. SecurityScheme}
                                        },
                      new List<string>()
                } });
            });



            //var loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    builder.AddConfiguration(Configuration.GetSection("Logging"));
            //    builder.AddConsole();
            //    builder.AddDebug();
            //    builder.AddFile(options =>
            //    {
            //        options.IncludeScopes = true;
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PostOpinion v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors("AllowAll");
            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            //    context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");

            //    await next.Invoke();
            //});
            loggerFactory.AddFile($@"{Directory.GetCurrentDirectory()}\Logs\log.txt");        
        }
    }
}
