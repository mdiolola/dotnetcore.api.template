using System.Reflection;
using System.Text;
using dotnetcore.api.template.Data;
using dotnetcore.api.template.Data.Interface;
using dotnetcore.api.template.Services;
using dotnetcore.api.template.Services.Interface;
using dotnetcore.api.template.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace dotnetcore.api.template
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = Assembly.GetExecutingAssembly().GetName().ToString().Split(',')[0] , Version = "v1" });
            });

            services.AddCors(options => options.AddPolicy("AllowAll ", p => p.AllowAnyOrigin()
                                                                           .AllowAnyMethod()
                                                                           .AllowAnyHeader()));


            services.AddMvc();
            services.AddAuthorization();
            services.AddOptions();

            //Authentication
            services.Configure<Authenticate>(Configuration.GetSection(nameof(Authenticate)));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["AuthSettings:Issuer"],
                        ValidAudience = Configuration["AuthSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["AuthSettings:SecurityKey"]))
                    };
                });

            services.AddScoped<IDbContext, ApiContext>();
            services.AddTransient<IAuthenticateService, AuthenticateService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI( c=>
            { c.SwaggerEndpoint("/swagger/v1/swagger.json", Assembly.GetExecutingAssembly().GetName().ToString().Split(',')[0]);
            });

            app.UseMvc();
        }
    }
}
