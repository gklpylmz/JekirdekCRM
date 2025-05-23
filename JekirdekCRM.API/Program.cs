using JekirdekCRM.DAL.Context;
using JekirdekCRM.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DBContext Service Injection
builder.Services.AddDbContext<MyContext>(opitons => opitons   
    .UseNpgsql(builder.Configuration.GetConnectionString("MyConnection")));

//Identity Injection
builder.Services.AddIdentity<User,IdentityRole<int>>(x =>
{
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireDigit = false;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
    x.Password.RequiredLength = 1;
}).AddEntityFrameworkStores<MyContext>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
