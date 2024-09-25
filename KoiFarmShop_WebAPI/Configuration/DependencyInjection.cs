using Microsoft.EntityFrameworkCore.Query.Internal;
using Repositories.Implement;
using Repositories.Interface;
using Services.Implement;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IKoiFishRepository, KoiFishRepository>();
            services.AddScoped<IKoiFishyRepository, KoiFishyRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartDetailRepository, CartDetailRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IConsignmentRepository, ConsignmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IKoiFishService, KoiFishService>();
            services.AddScoped<IKoiFishyService, KoiFishyService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartDetailService, CartDetailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IConsignmentService, ConsignmentService>();
            services.AddScoped<IUserServices, UserService>();
            return services;
        }
    }
}
