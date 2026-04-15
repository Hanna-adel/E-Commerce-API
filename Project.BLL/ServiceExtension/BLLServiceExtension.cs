using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.BLL.ServiceExtension
{
    public static class BLLServiceExtension
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateValidator>();
            services.AddScoped<IValidator<CategoryCreateDto>, CategoryCreateValidator>();
            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidator>();
            services.AddScoped<IValidator<CategoryUpdateDto>, CategoryUpdateValidator>();

        }
    }
}
