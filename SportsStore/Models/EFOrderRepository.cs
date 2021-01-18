using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models {
    public class EFOrderRepository : IOrderRepository {
        public EFOrderRepository(ApplicationDbContext applicationDbContext) {
            ApplicationDbContext = applicationDbContext;
        }

        public ApplicationDbContext ApplicationDbContext { get; set; }

        public void SaveOrder(Order order) {
            ApplicationDbContext.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0) ApplicationDbContext.Orders.Add(order);

            ApplicationDbContext.SaveChanges();
        }

        public IQueryable<Order> Orders => ApplicationDbContext
            .Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);
    }
}