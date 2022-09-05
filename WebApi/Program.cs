using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");


// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

//builder.Services.AddDefaultIdentity<IdentityUser>(
//    opt=>
//    {
//        opt.Password.RequireDigit = true;
//        opt.Password.RequiredLength = 5;
//        opt.Password.RequireUppercase = true;
//        opt.Lockout.MaxFailedAccessAttempts = 5;
//        opt.User.RequireUniqueEmail = true;
//        opt.SignIn.RequireConfirmedEmail = false;
//    })
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<DataContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
