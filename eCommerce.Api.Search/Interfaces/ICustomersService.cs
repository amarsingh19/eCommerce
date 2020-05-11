using eCommerce.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSucess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
    }
}
