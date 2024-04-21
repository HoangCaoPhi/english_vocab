using Asp.Versioning;
using EnglishVocab.Application.Models;
using EnglishVocab.BussinessApi.Middleware;
using EnglishVocab.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(op =>
{
    op.Filters.Add(typeof(CustomExceptionFilter));
});

// Add Newston Json Config
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateFormatHandling = ConvertConfig.JSONDateFormatHandling;
        options.SerializerSettings.DateTimeZoneHandling = ConvertConfig.JSONDateTimeZoneHandling;
        options.SerializerSettings.DateFormatString = ConvertConfig.JSONDateFormatString;
        options.SerializerSettings.NullValueHandling = ConvertConfig.JSONNullValueHandling;
        options.SerializerSettings.ReferenceLoopHandling = ConvertConfig.JSONReferenceLoopHandling;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    }
);

builder.Services.AddApiVersioning(config =>
{
    // Specify the default API Version as 1.0
    config.DefaultApiVersion = new ApiVersion(1, 0);
    // If the client hasn't specified the API version in the request, use the default API version number 
    config.AssumeDefaultVersionWhenUnspecified = true;
    // Advertise the API versions supported for the particular endpoint
    config.ReportApiVersions = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Applying Migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
