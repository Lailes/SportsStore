using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models {
    public class EFOrderRepository: IOrderRepository {

        public ApplicationDbContext ApplicationDbContext { get; set; }

        public EFOrderRepository(ApplicationDbContext applicationDbContext) {
            ApplicationDbContext = applicationDbContext;
        }

        public void SaveOrder(Order order) {
            ApplicationDbContext.AttachRange(order.Lines.Select(l=>l.Product));
            if (order.OrderID == 0) {
                ApplicationDbContext.Orders.Add(order);
            }

            ApplicationDbContext.SaveChanges();
        }

        public IQueryable<Order> Orders => ApplicationDbContext
            .Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);
    }
}
