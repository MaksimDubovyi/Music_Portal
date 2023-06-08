using WebApp_Music_Portal.Models;
using Microsoft.EntityFrameworkCore;
using WebApp_Music_Portal.Repository;

var builder = WebApplication.CreateBuilder(args);
// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<Music_Portal_Context>(options => options.UseSqlServer(connection));


builder.Services.AddDistributedMemoryCache();
// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCounterService();


var app = builder.Build();
app.UseStaticFiles();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");

app.Run();