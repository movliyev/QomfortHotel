

using Hotel_Managment.Application.Abstractions.Repositories;
using Hotel_Managment.Application.Abstractions.Services;
using Hotel_Managment.Domain.Entities;
using Hotel_Managment.Rersistance.DAL;
using Hotel_Managment.Rersistance.Implementations.Repositories;
using Hotel_Managment.Rersistance.Implementations.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace ProniaOnion202.Persistance.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceService(this IServiceCollection services,IConfiguration conf)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conf.GetConnectionString("Default"),
                b=>b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));
           services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //services.AddMvc(config =>
            //{
            //    var policy=new AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomService, RoomService>();

            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IProductService, ProductService>();

            //services.AddScoped<IColorRepository, ColorRepository>();
            //services.AddScoped<IColorService, ColorService>();
            //services.AddScoped<IUserService, UserService>();

            return services;

        }
    }
}
