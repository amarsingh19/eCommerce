using eCommerce.Api.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
