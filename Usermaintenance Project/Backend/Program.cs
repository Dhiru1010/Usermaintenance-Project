using Domain3.Model;
using Microsoft.EntityFrameworkCore;
using UsermaintenanceUsingWebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Configuration = builder.Configuration;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddDbContext<DhiruDBUserMaintenanceWebContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<ICountryRepository,CountryRepository>();
builder.Services.AddScoped<IStateRepository,StateRepository>();
builder.Services.AddScoped<IUserSQRepository,UserSQRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader()));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
