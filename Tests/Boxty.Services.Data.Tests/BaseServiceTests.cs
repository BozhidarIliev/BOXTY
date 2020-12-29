namespace Boxty.Services.Data.Tests
{
    using Boxty.Data;
    using Boxty.Data.Common;
    using Boxty.Data.Common.Repositories;
    using Boxty.Data.Models;
    using Boxty.Data.Repositories;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Services.Interfaces;
    using Boxty.Services.Mapping;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Reflection;

    public abstract class BaseServiceTests : IDisposable
    {
        public BaseServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.BoxtyDbContext = this.ServiceProvider.GetRequiredService<BoxtyDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected BoxtyDbContext BoxtyDbContext { get; set; }

        public void Dispose()
        {
            this.SetServices();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<BoxtyDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddIdentityCore<BoxtyUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<BoxtyDbContext>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<ITableItemService, TableItemService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderItemService, OrderItemService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IDriverService, DriverService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            return services;
        }
    }
}