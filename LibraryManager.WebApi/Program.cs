using LibraryManager.Application.DTOs.Response;
using LibraryManager.Application.Extension;
using LibraryManager.Infrastructure.Extension;
using LibraryManager.WebApi.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetValue<string>("ConnectionStrings:InvoiceGeneratorConnectionString"),
        sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
        {
            TableName = "tbl_Logs",
            AutoCreateSqlTable = true
        })
    .MinimumLevel.Information()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext().CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Library Management API",
            Version = "v1",
            Description = "This API provides endpoints for managing books in a library system, including operations for adding, retrieving, updating, and deleting book records. It enforces validation rules for book metadata such as title, author, publisher, ISBN, and pricing.",
            Contact = new OpenApiContact
            {
                Name = "Library Admin",
                Email = "admin@librarysystem.com"
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true); ;
    });

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var response = new ApiResponse()
            {
                Status = false,
                Message = "Validation Failed",
                Response=errors,
                StatusCode=(int)HttpStatusCode.BadRequest,

            };

            return new ObjectResult(response);
        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAllOrgins");
app.UseMiddleware<GlobalExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
