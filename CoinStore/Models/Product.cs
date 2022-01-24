using System.ComponentModel.DataAnnotations.Schema;

namespace CoinStore.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //[Column(TypeName = "decimal(8, 2")]
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
