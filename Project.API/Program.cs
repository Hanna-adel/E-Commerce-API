using Microsoft.Extensions.FileProviders;
using Project.BLL;
using Project.BLL.ServiceExtension;
using Project.DAL;
using Scalar.AspNetCore;
namespace Project.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var rootPath = builder.Environment.ContentRootPath;
            var staticFilepath = Path.Combine(rootPath, "Files");
            if (!Directory.Exists(staticFilepath))
            {
                Directory.CreateDirectory(staticFilepath);
            }
            builder.Services.Configure<StaticFileOptions>(cfg =>
            {
                cfg.FileProvider = new PhysicalFileProvider(staticFilepath);
                cfg.RequestPath = "/Files";
            });

            builder.Services.AddDALServices(builder.Configuration);
            builder.Services.AddBLLServices(builder.Configuration);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
