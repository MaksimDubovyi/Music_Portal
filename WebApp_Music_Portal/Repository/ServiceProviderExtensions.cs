namespace WebApp_Music_Portal.Repository
{
    public static class ServiceProviderExtensions
    {
        public static void AddCounterService(this IServiceCollection services)
        {
            services.AddScoped<IRepository_User, Repository_User>();
            services.AddScoped<IRepository_Music, Repository_Music>();
            services.AddScoped<IRepository_Admin, Repository_Admin>();
            services.AddScoped<IRepository_Genre, Repository_Genre>();
        }
    }
}
