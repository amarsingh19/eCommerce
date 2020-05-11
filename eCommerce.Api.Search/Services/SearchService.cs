using eCommerce.Api.Search.Interfaces;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            await Task.Delay(2);
            return (true, new { Message = "Hello" });
        }
    }
}
