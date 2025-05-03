using InventoryManagement.API.Configuration;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Repository;
using InventoryManagement.Services.IServices;
using InventoryManagement.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InventoryManagement.Infrastructure;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
     

        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IAuthServices, AuthServices>();
        builder.Services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();
        builder.Services.AddScoped<IReportService, ReportService>();


        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

   
        builder.Services.AddControllers();

      
        builder.Services.ConfigureIdentity(builder.Configuration);

    
        builder.Services.ConfigureJwtToken(builder.Configuration);

  
        builder.Services.ConfigureSwagger();


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", builder =>
            {
                builder.WithOrigins()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

   

        var app = builder.Build();

      
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowSpecificOrigins");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}
