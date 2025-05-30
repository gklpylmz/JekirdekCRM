using JekirdekCRM.API.Tools;
using JekirdekCRM.BLL.ManagerServices.Abstracts;
using JekirdekCRM.BLL.ManagerServices.Concretes;
using JekirdekCRM.DAL.Context;
using JekirdekCRM.DAL.Repositories.Abstracts;
using JekirdekCRM.DAL.Repositories.Concretes;
using JekirdekCRM.ENTITIES.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//JWT 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = JwtTokenDefaults.ValidAudience,
        ValidIssuer = JwtTokenDefaults.ValidIssuer,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

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

//RepositoryManagerServices
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IManager<>), typeof(BaseManager<>));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerManager, CustomerManager>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
