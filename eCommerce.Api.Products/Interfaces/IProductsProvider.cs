using eCommerce.Api.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
