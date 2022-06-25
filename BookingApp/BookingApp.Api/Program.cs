
using BookingApp.Api.Middlewares;
using BookingApp.Api.Services;
using BookingApp.Dal;
using BookingApp.Dal.Repositories;
using BookingApp.Domain.Abstraction.Repositories;
using BookingApp.Domain.Abstraction.Services;
using BookingApp.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DataSource>();
builder.Services.AddSingleton<MyFirstServices>();
builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(options => { options.UseSqlServer(connectionString); });
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IHotelsRepository,HotelRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseDateTimeHeader();

app.MapControllers();


//app.Use(async  (context,next) => {
//      add code here for the request
//      await next();
//      here the code for the response 
//});



app.Run();
