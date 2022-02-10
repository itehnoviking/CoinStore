using CoinStore.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinStore
{
    public class Program
    {

        public static void Main(string[] args)
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
            //builder.Services.AddServerSideBlazor();

            //builder.Services.AddServerSideBlazor().AddCircuitOptions(o => {
            //    o.DetailedErrors = _env.IsDevelopment;
            //}).AddHubOptions(opt => {
            //    opt.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10MB
            //});

            builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });





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

            //app.UseEndpoints(endpoints => {
            //    endpoints.MapControllerRoute("catpage",
            //        "{category}/Page{productPage:int}",
            //        new { Controller = "Home", action = "Index" });

            //    endpoints.MapControllerRoute("page", "Page{productPage:int}",
            //        new { Controller = "Home", action = "Index", productPage = 1 });

            //    endpoints.MapControllerRoute("category", "{category}",
            //        new { Controller = "Home", action = "Index", productPage = 1 });

            //    endpoints.MapControllerRoute("pagination",
            //        "Products/Page{productPage}",
            //        new { Controller = "Home", action = "Index", productPage = 1 });
            //    endpoints.MapDefaultControllerRoute();
            //    endpoints.MapRazorPages();
            //    endpoints.MapBlazorHub();
            //    endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
            //});


            app.MapControllerRoute("catpage", "{category}/Page{productPage:int}", new { controller = "Home", action = "index" });
            app.MapControllerRoute("page", "Page{productPage:int}", new { controller = "Home", action = "index", productPage = 1 });
            app.MapControllerRoute("category", "{category}", new { controller = "Home", action = "index", productPage = 1 });
            app.MapControllerRoute("pagination", "Products/Page{productPage}", new { controller = "Home", action = "index", productPage = 1 });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapDefaultControllerRoute();
            app.MapRazorPages();
            app.MapBlazorHub();
            app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");



            SeedData.EnsurePopulated(app);

            app.Run();

        }
    }
}
