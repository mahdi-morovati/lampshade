﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ShopManagement.Configuration;

public class ShopManagementBootstrapper
{
    /**
     * به جای اینکه برم تو فایل Program.cs و اونجا بنویسم
     * AddTransient<IProductCategory, ProductCategory>
     * هر ماژول کانفیگوریشنش رو داخل خودش داشته باشه
     */
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
        services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

        services.AddTransient<IProductApplication, ProductApplication>();
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
    }
}