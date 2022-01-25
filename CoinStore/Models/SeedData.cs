using Microsoft.EntityFrameworkCore;

namespace CoinStore.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange
                    (

                    new Product
                    {
                        Name = "50 kopeek 1922 g.",
                        Description = "VF, 26.67mm, 10 gr, Ag 900",
                        Category = "SSSR",
                        Price = 1650M
                    },

                    new Product
                    {
                        Name = "20 kopeek 1925 g.",
                        Description = "VF, 21.8mm, 3.6 gr, Ag 500",
                        Category = "SSSR",
                        Price = 280M
                    },

                    new Product
                    {
                        Name = "10 kopeek 1923 g.",
                        Description = "VF, 17.27mm, 1.8 gr, Ag 500",
                        Category = "SSSR",
                        Price = 280M
                    },

                    new Product
                    {
                        Name = "50 kopeek 1924 g.",
                        Description = "VF, 26.67mm, 10 gr, Ag 900",
                        Category = "SSSR",
                        Price = 1250M
                    },

                    new Product
                    {
                        Name = "1 kopeyka serebrom 1844 g. E.M.",
                        Description = "VF, 24mm, 10.24 gr, Cu",
                        Category = "Russian Empire",
                        Price = 1050M
                    }

                    );

                context.SaveChanges();
            }
        }

    }
}
