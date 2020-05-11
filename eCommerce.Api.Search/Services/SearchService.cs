using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Models;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await customersService.GetCustomersAsync();
            const string notAvailable = "<Not Available>";
            if (ordersResult.IsSuccess)
            {
                foreach(var order in ordersResult.Orders)
                {
                    order.CustomerName = customersResult.IsSucess ? customersResult.Customers.FirstOrDefault(c => c.Id == order.CustomerId)?.Name : notAvailable;
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSucess ? productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            notAvailable;
                    }
                }
                var result = new
                {
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
