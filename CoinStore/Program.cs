using CoinStore.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinStore
{
    public class Program
    {

        public static async Task Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<StoreDbContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
            builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
            builder.Services.AddRazorPages();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();
            //app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            
            app.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { controller = "Home", action = "index" });
            app.MapControllerRoute("page", "Page{productPage:int}", new { controller = "Home", action = "index", productPage = 1 });
            app.MapControllerRoute("category", "{category}", new { controller = "Home", action = "index", productPage = 1 });
            app.MapControllerRoute("pagination", "Products/Page{productPage}", new { controller = "Home", action = "index", productPage = 1 });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();


            SeedData.EnsurePopulated(app);

            app.Run();

        }
    }
}
