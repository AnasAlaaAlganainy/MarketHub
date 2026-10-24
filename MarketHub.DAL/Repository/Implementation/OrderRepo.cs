using MarketHub.DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.DAL.Repository.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll(string userId)
        => _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId && o.OrderStatus.ToLower() != "canceled")
            .ToList();

        public Order GetById(string userId, int orderId)
        => _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .SingleOrDefault(o => o.UserId == userId && o.Id == orderId && o.OrderStatus.ToLower() != "canceled");

        public bool CreateOrder(Order userOrder)
        {
            _context.Orders.Add(userOrder);
            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }

        public bool CreateOrderItems(List<OrderItem> orderItems)
        {
            _context.OrderItems.AddRange(orderItems);
            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }

        public Order GetOrderByIdAndUserId(int orderId, string userId)
        => _context.Orders
            .Where(o => o.Id == orderId && o.UserId == userId && o.OrderStatus.ToLower() != "canceled")
            .FirstOrDefault();
        public List<Order> GetOrdersByStatus(string userId, string status)
            => _context.Orders.Include(o => o.OrderItems).Where(o => o.OrderStatus == status && o.UserId == userId).ToList();
        public void BeginTransaction()
            => _context.Database.BeginTransaction();
        public void CommitTransaction() 
            => _context.Database.CurrentTransaction?.Commit();
        public void RollbackTransaction()
            => _context.Database.CurrentTransaction?.Rollback();
        public bool SaveChanges()
            => _context.SaveChanges() > 0;
    }
}
