using Microsoft.EntityFrameworkCore;

namespace CoinStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly Guid guid = new Guid("00000000000000000000000000000000"); 
        private StoreDbContext _context;

        public EFOrderRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders.Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if(order.OrderId == guid)
            {
                _context.Orders.Add(order);
            }

            _context.SaveChanges();
        }
    }
}
