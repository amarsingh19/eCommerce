using eCommerce.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
