using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Text;
using Tebnabawe.Application.AboutT;
using Tebnabawe.Application.AudioT;
using Tebnabawe.Application.Authentication;
using Tebnabawe.Application.Bases;
using Tebnabawe.Application.BrochuresT;
using Tebnabawe.Application.Configuration;
using Tebnabawe.Application.GoalsT;
using Tebnabawe.Application.IslamicBooksT;
using Tebnabawe.Application.NewsT;
using Tebnabawe.Application.PhotosLibraryT;
using Tebnabawe.Application.VideoT;
using Tebnabawe.Application.WorkT;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Web
{
    public class Startup
    {
        readonly string CORSOpenPolicy = "OpenCORSPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JWT>(Configuration.GetSection("JWT"));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<TebnabaweContext>().AddDefaultTokenProviders();


            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddControllers();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
            });
            services.AddAutoMapper(typeof(MapperConfig));
            services.AddTransient<AboutAppService>();
            services.AddTransient<GoalsAppService>();
            services.AddTransient<WorkAppService>();
            services.AddTransient<NewsAppService>();
            services.AddTransient<BrochuresAppService>();
            services.AddTransient<IslamicBooksAppService>();
            services.AddTransient<PhotosLibraryAppService>();
            services.AddTransient<AudiosChannelService>();
            services.AddTransient<AudiosLibraryService>();
            services.AddTransient<VideosLibraryService>();
            services.AddTransient<VideosChannelService>();
            services.AddTransient<RequestRadioService>();
            services.AddTransient<RadioDataService>();

            services.AddDbContext<TebnabaweContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tebnabawe.Web", Version = "v1" });
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                };
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORSOpenPolicy, builder => {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddHttpContextAccessor();
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            DataInitializer.SeedData(userManager,roleManager);
            app.UseRouting();
            app.UseCors(CORSOpenPolicy);
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            

        }
    }
}
