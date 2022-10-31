using Microsoft.EntityFrameworkCore;
using WebApi.Data;


var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

//var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
//var options = optionsBuilder.UseSqlServer(connection).Options;
//DataContext dataContext = new DataContext(options);

//builder.Services.Add(new ServiceDescriptor(typeof(DataContext), dataContext));

// добавляем контекст ApplicationContext в качестве сервиса в приложение

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

var host = builder.Configuration.GetSection("Host").Value;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
