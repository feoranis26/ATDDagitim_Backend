using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using ATDBackend.Database.DBContexts;
using ATDBackend.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ATDBackend
{
    public class Program
    {
        private static void InitModules(ILogger logger, IConfiguration config)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            List<Type> moduleTypes = asm.GetTypes()
                .Where(
                    x =>
                        x.IsClass
                        && x.IsAbstract
                        && x.IsSealed
                        && x.Namespace == "ATDBackend.Modules"
                )
                .ToList();

            int sucCount = 0;
            foreach (Type moduleType in moduleTypes)
            {
                moduleType
                    .GetProperties()
                    .Where(x => x.CanWrite && x.PropertyType == typeof(IConfiguration))
                    .ToList()
                    .ForEach(x => x.SetValue(null, config));
                moduleType
                    .GetProperties()
                    .Where(x => x.CanWrite && x.PropertyType == typeof(ILogger))
                    .ToList()
                    .ForEach(x => x.SetValue(null, logger));

                MethodInfo? initmethod = moduleType.GetMethod("Initialize");
                if (
                    initmethod == null
                    || !initmethod.IsStatic
                    || initmethod.ReturnType != typeof(bool)
                )
                {
                    logger.Log(
                        LogLevel.Error,
                        $"The module {moduleType.Name} does not contain a proper Initialize method (2)"
                    );
                    continue;
                }
                bool? suc = null;

                try
                {
                    suc = (bool?)initmethod.Invoke(null, null);
                }
                catch (Exception ex)
                {
                    logger.Log(
                        LogLevel.Error,
                        $"The module {moduleType.Name} failed to initialize"
                    );
                    continue;
                }

                if (suc == null)
                {
                    logger.Log(
                        LogLevel.Error,
                        $"The module {moduleType.Name} does not contain a proper Initialize method (2)"
                    );
                    continue;
                }

                if (!suc.Value)
                    logger.Log(
                        LogLevel.Error,
                        $"The module {moduleType.Name} failed to initialize"
                    );
                else
                {
                    logger.Log(LogLevel.Information, $"Initialized {moduleType.Name}");
                    sucCount++;
                }
            }

            logger.Log(LogLevel.Information, $"Initialized {sucCount}/{moduleTypes.Count} modules");
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //CORS Policy
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder
                .Services
                .AddCors(options =>
                {
                    options.AddPolicy(
                        name: MyAllowSpecificOrigins,
                        policy =>
                        {
                            policy
                                .WithOrigins(
                                    "https://sehirbahceleri.com.tr",
                                    "https://www.sehirbahceleri.com.tr",
                                    "https://*.sehirbahceleri.com.tr",
                                    "http://localhost:3000",
                                    "http://127.0.0.1:3000",
                                    "http://localhost",
                                    "127.0.0.1",
                                    "localhost"
                                )
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                        }
                    );
                });
            //DB Context
            var conn = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            builder.Services.AddDbContext<AppDBContext>(options => options.UseNpgsql(conn)); //Default DB context

            //Identity
            builder
                .Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWTToken:Issuer"],
                        ValidAudience = builder.Configuration["JWTToken:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECURITYKEY"))
                        ),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder
                .Services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Version = "v1",
                            Title = "Sehirbahceleri API",
                            Description = "Backend API for https://sehirbahceleri.com.tr",
                            TermsOfService = new Uri("https://www.sehirbahceleri.com.tr"),
                            Contact = new OpenApiContact
                            {
                                Name = "Contact",
                                Url = new Uri("https://www.sehirbahceleri.com.tr/")
                            },
                            License = new OpenApiLicense
                            {
                                Name = " License",
                                Url = new Uri("https://www.sehirbahceleri.com.tr")
                            }
                        }
                    );

                    var filePath = Path.Combine(AppContext.BaseDirectory, "ATDBackend.xml");
                    options.IncludeXmlComments(filePath);
                });
            builder
                .Services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


            builder.Services.AddControllersWithViews().AddJsonOptions(options =>
               options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                          );

            var app = builder.Build();

            InitModules(app.Logger, app.Configuration);




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/v1/swagger.json", "Web API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
