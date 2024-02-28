using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data;
using WebAPI.Data.Interfaces;
using WebAPI.Data.Repo;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Interface;
using WebAPI.Middlewares;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddAutoMapper(typeof(Automapper).Assembly);
builder.Services.AddScoped<IUnitofWork,UnitOfWork>();
builder.Services.AddScoped<IPhotoService,PhotoService>();
builder.Services.AddDbContext<Dbclass>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var secretKey=builder.Configuration.GetSection("AppSettings:key").Value;
var key=new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(secretKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt=>{
    opt.TokenValidationParameters=new TokenValidationParameters{
        ValidateIssuerSigningKey=true,
        ValidateIssuer=false,
        ValidateAudience=false,
        IssuerSigningKey=key
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(m=>m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
