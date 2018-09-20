using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NGH.API.DataAccess;
using Microsoft.EntityFrameworkCore;
using NGH.API.DataAccess.UnitOfWork;
using NGH.API.DataAccess.Repositories;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NGH.API.Services;
using Microsoft.IdentityModel.Tokens;
using NGH.API.Controllers;

namespace NGH.API
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeliveryNoteRepository, DeliveryNoteRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();
            
            services.AddDbContext<NGHDataContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddAutoMapper();
            
            // configure jwt authentication
            /*var secret = Configuration.GetSection("AppSettings:Secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.Events = new JwtBearerEvents 
                {
                    OnTokenValidated = context => 
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<UserController>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });*/
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Secret").Value);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {                
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddMvc().AddJsonOptions(opt => {
                opt.SerializerSettings.ReferenceLoopHandling = 
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
