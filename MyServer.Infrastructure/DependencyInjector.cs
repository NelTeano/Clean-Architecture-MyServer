﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyServer.Core.Interfaces;
using MyServer.Infrastructure.Configurations;
using MyServer.Infrastructure.Data;
using MyServer.Infrastructure.Repositories;
using MyServer.Infrastructure.Services;


namespace MyServer.Infrastructure
{
    public static class DependencyInjector
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Register the DbContext
            services.AddDbContext<ApplicationContextDB>(options =>
                options.UseSqlServer(connectionString));

            // Configure Stripe settings
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));

            // Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Register services with interfaces
            services.AddScoped<IPaymentService, StripePaymentService>();



            return services;
        }
    }
}
