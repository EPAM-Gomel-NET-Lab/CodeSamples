using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_Task_Standard.Entities;
using EF_Task_Standard.Models;
using EF_Task_Standard.Repository;
using EF_Task_Standard.Scope;
using Microsoft.EntityFrameworkCore;

namespace EF_Task_Standard
{
    public interface IProductCategoryService
    {
        List<ProductCategoryInfo> GetProductInfoForCategory(int categoryId);

        Task<List<ProductCategoryInfo>> GetProductInfoForCategoryAsync(int categoryId);
    }


    internal class ProductCategoryService : IProductCategoryService
    {
        private readonly IScopeCreator _scopeCreator;
        private readonly IEntityStorage<Order> _orders;
        private readonly IEntityStorage<Product> _products;
        private readonly IEntityStorage<Customer> _customers;
        private readonly IEntityStorage<OrderDetails> _orderDetails;

        public ProductCategoryService(
            IScopeCreator scopeCreator,
            IEntityStorage<Order> orders,
            IEntityStorage<Product> products,
            IEntityStorage<Customer> customers,
            IEntityStorage<OrderDetails> orderDetails)
        {
            _orders = orders;
            _products = products;
            _customers = customers;
            _scopeCreator = scopeCreator;
            _orderDetails = orderDetails;
        }

        public List<ProductCategoryInfo> GetProductInfoForCategory(int categoryId)
        {
            using (_scopeCreator.CreateReadonly())
            {
                return ProductCategoryInfoQuery(categoryId).ToList();
            }
        }

        public async Task<List<ProductCategoryInfo>> GetProductInfoForCategoryAsync(int categoryId)
        {
            using (_scopeCreator.CreateReadonly())
            {
                return await ProductCategoryInfoQuery(categoryId).ToListAsync();
            }
        }

        private IQueryable<ProductCategoryInfo> ProductCategoryInfoQuery(int categoryId)
        {
            var query = from orders in _orders.GetAll()
                        join orderDetail in _orderDetails.GetAll() on orders.OrderID equals orderDetail.OrderID
                        join customer in _customers.GetAll() on orders.CustomerID equals customer.CustomerID
                        join product in _products.GetAll() on orderDetail.ProductID equals product.ProductID
                        where product.CategoryID == categoryId
                        orderby product.ProductName
                        select new ProductCategoryInfo
                        {
                            OrderID = orders.OrderID,
                            ProductName = product.ProductName,
                            ContactName = customer.ContactName,
                            UnitPrice = orderDetail.UnitPrice,
                            Quantity = orderDetail.Quantity,
                            Discount = orderDetail.Discount
                        };

            return query;
        }
    }
}
