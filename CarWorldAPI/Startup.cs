using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using DatabaseAccess.Entities;
using DatabaseAccess.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Services;
using Services.Services;
using DatabaseAccess.UnitOfWorks;
using Utility;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CorePush.Google;
using CorePush.Apple;
using Utility.Models;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace CarWorldAPI
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.WithOrigins("https://car-world-react-project.herokuapp.com",
                                        "https://cw-react-project.herokuapp.com",
                                        "http://localhost:3000",
                                        "https://www.carworld.tokyo",
                                        "https://www.carworldnews.tokyo")
                            .AllowAnyHeader().AllowAnyMethod(); //set up localhost for frontend
                });
            });
            //add mail
            services.Configure<MailSetting>(Configuration.GetSection("MailSettings"));
            //add dbcontext
            var connection = Configuration.GetConnectionString("CarWorld");
            services.AddDbContext<CarWorldContext>(
                options => options.UseSqlServer(connection));
            //services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
            //        .AddEntityFrameworkStores<CarWorldContext>();
            //add DI
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<JWTService>();
            services.AddTransient<UserService>();
            services.AddTransient<BrandService>();
            services.AddTransient<CarModelService>();
            services.AddTransient<AttributionService>();
            services.AddTransient<GenerationAttributionService>();
            services.AddTransient<GenerationService>();
            services.AddTransient<EngineTypeService>();
            services.AddTransient<AccessoryService>();
            services.AddTransient<ProposalService>();
            services.AddTransient<ContestEventService>();
            services.AddTransient<PrizeService>();
            services.AddTransient<ContestPrizeService>();
            services.AddTransient<CERegisterService>();
            services.AddTransient<ExchangeService>();
            services.AddTransient<ExchangeResponseService>();
            services.AddTransient<PostService>();
            services.AddTransient<FeedbackService>();
            services.AddTransient<AddressService>();
            services.AddTransient<NotificationService>();
            services.AddTransient<MailService>();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();
            
            services.AddControllers();


            var appSettingsSection = Configuration.GetSection("FcmNotification");
            services.Configure<FcmNotificationSetting>(appSettingsSection);

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //Authorization region
            //services.AddAuthorization(options =>
            //{
            //    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
            //        JwtBearerDefaults.AuthenticationScheme);

            //    defaultAuthorizationPolicyBuilder =
            //        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

            //    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

            //});

            
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("FirebaseSDK.json")
            });
            
            //Authentication region
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });

            //Swagger region
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger CarWorld", Version = "V1" });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme"
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
        }

        [EnableCors("AllowOrigin")]
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger CarWorld V1");
                c.RoutePrefix = string.Empty;

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();
            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
