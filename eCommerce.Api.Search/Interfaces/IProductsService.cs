using eCommerce.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSucess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
