// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.EntityFrameworkCore;
// using restapi.Data;


// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// // Configure DbContext
// builder.Services.AddDbContext<ApplicationDBcontext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// //builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve; });

// builder.Services.AddRazorPages();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// // Add authentication & authorization
// app.UseAuthentication();
// app.UseAuthorization();

// app.MapRazorPages();
// app.MapControllers();

// app.Run();
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using restapi.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "REST API",
        Version = "v1",
        Description = "An ASP.NET Core Web API"
    });
});

// Configure DbContext
builder.Services.AddDbContext<ApplicationDBcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JSON serialization
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// Enable Swagger for both Development and Production
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API V1");
    c.RoutePrefix = string.Empty; // Make Swagger UI the default page
});

// Configure HTTPS redirection and security
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Configure authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Configure endpoints
app.MapControllers();

app.Run();