using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.Helpers;
using WebApi.Models;


var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

//var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
//var options = optionsBuilder.UseSqlServer(connection).Options;
//DataContext dataContext = new DataContext(options);

//builder.Services.Add(new ServiceDescriptor(typeof(DataContext), dataContext));

// ��������� �������� ApplicationContext � �������� ������� � ����������

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
