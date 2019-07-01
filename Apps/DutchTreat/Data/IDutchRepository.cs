using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByCategory(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOrders( bool includeItems);
        IEnumerable<Order> GetAllOrdersByUsername(string username, bool includeItems);

        //Order GetOrderById(int id);
        void AddEntity(object model);
        Order GetOrderById(string name, int orderId);
        void AddOrder(Order newOrder);
    }
}