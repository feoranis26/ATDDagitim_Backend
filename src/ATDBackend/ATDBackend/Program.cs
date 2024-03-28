using System.Text;
using ATDBackend.Database.DBContexts;
using ATDBackend.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ATDBackend
{
    public class Program
    {
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
                                .WithOrigins("https://*.sehirbahceleri.com.tr")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
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
                            Encoding.UTF8.GetBytes(builder.Configuration["JWTToken:SecurityKey"])
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
                });
            builder
                .Services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); //FOR DEVELOPMENT ONLY
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
