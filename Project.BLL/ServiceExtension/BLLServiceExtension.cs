using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Project.BLL.ServiceExtension
{
    public static class BLLServiceExtension
    {
        public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IErrorMapper, ErrorMapper>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IImageManager, ImageManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateValidator>();
            services.AddScoped<IValidator<CategoryCreateDto>, CategoryCreateValidator>();
            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidator>();
            services.AddScoped<IValidator<CategoryUpdateDto>, CategoryUpdateValidator>();
            services.AddScoped<IValidator<ImageUploadDto>, ImageUploadDtoValidator>();
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<AppDbContext>();
            services .Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };

            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin"));
            });

        }
    }
}
