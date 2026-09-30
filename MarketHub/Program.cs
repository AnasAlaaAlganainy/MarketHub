using MarketHub.API.Extention_Methods;
using MarketHub.DAL.Utils.Abstraction;
using Microsoft.OpenApi.Models;
using static System.Net.WebRequestMethods;

namespace MarketHub.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //DI Register---------
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration
                        .GetConnectionString("MarketHub"));
            });

            builder.Services.DIConfigForDAL();
            builder.Services.DIConfigForBLL();

            builder.Services.AddIdentity<AppUser, IdentityRole>( options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            //DI Register---------
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo()
                    {
                        Version = "v1",
                        Title = "MarketHub Api",
                        Description = "My First Api Web App",
                        Contact = new OpenApiContact()
                        {
                            Name = "Anas Alaa",
                            Email = "anasalaa4869@gmail.com",
                            Url = new Uri("https://mydomain.com"),
                        }
                    });
                    options.AddSecurityDefinition(
                        "Bearer", new OpenApiSecurityScheme()
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description = "Enter The JWT Key "
                        });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement(){
                                {
                                    new OpenApiSecurityScheme()
                                    {
                                        Reference = new OpenApiReference()
                                        {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                        },
                                        Name = "Bearer",
                                        In = ParameterLocation.Header,
                                    },
                                    new List<string>()
                                }
                            });
                });
            ////JWT Service Configuration 
            builder.Services.AddJwtAuthConfigurations(builder.Configuration);
            //builder.Services.AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.SaveToken = true;
            //        options.RequireHttpsMetadata = false;

            //        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            //        {
            //            ValidateIssuer = true,
            //            ValidIssuer = "http://localhost:5182",
            //            ValidateAudience = false,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes())
            //        };
            //    });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            //Data Seeding
            using (var scope = app.Services.CreateScope())
            {
                var seedData = scope.ServiceProvider.GetRequiredService<ISeedData>();

                await seedData.IdentityDataSeedingAsync();
                await seedData.DataSeedingAsync();
            }

            app.UseHttpsRedirection();
            // JWT Configuration
            //; ; ; ; ; ; ; ; ; ; ; ;
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}





