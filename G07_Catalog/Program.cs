using CatalogRepository;
using G07_Catalog.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;


namespace G07_Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 
           
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/log-.txt")
            .CreateLogger();
            builder.Host.UseSerilog();

            var app = builder.Build();
            

            app.UseSerilogRequestLogging();
            
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    Log.Error(exception, "An error occurred while processing the request.");

                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/plain";

                    await context.Response.WriteAsync("An error occurred while processing your request.");
                });
            });

            //daamatet shecdomebis damushaveba. 
            //momxdari shecdoma unda chaweros textur failshi(daalogiros)
            //xolo klients daubrunos kodi 500.
            app.MapGet("/categories", (HttpContext httpContext) =>
            {
                try
                {
                 using CatalogDbContext context = new();
                var categories = context.Categories
                    .OrderBy(x => x.CategoryName)
                    .ToList();
                var response = categories
                    .Select(x => new CategoryModel(x.CategoryId, x.CategoryName));

                return response;
                }
                catch (Exception ex)
                {

                    Log.Error(ex, $"An error occurred while retrieving the categories from the database.");
                    throw new Exception($"An error occurred while retrieving the categories from the database.", ex);
                }
               
            });

            //daamatet shecdomebis damushaveba. 
            //momxdari shecdoma unda chaweros textur failshi(daalogiros)
            //xolo klients daubrunos kodi 500.
            app.MapGet("/products/{id:int}", (HttpContext httpContext, int id) =>
            {
                //int id = Convert.ToInt32(httpContext.Request.RouteValues["id"]);
                try
                {
                using CatalogDbContext context = new();
                var products = context.Products
                    .OrderBy(x => x.ProductName)
                    .Include(x => x.Category)
                    .Where(x => x.CategoryId == id)
                    .ToList();

                var response = products.Select(x => new ProductModel(
                    x.ProductId,
                    x.ProductName,
                    x.Category!.CategoryName,
                    x.QuantityPerUnit,
                    x.UnitPrice,
                    x.UnitsInStock,
                    x.UnitsOnOrder,
                    x.ReorderLevel,
                    x.Discontinued));

                return response;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occurred while retrieving the products for category with ID '{id}' from the database.");
                    throw;
                }


            });

            app.Run();
            
        }
    }
}