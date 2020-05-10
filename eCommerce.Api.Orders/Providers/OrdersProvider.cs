namespace eCommerce.Api.Orders.Providers
{
    #region <Usings>
    using AutoMapper;
    using eCommerce.Api.Orders.Db;
    using eCommerce.Api.Orders.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    #endregion

    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Orders.Any() || dbContext.Orders.Count() == 0)
            {
                dbContext.Orders.Add(new Db.Order() { Id = 1, OrderDate = DateTime.Now.AddDays(-1),CustomerId = 1, Total = 2500 });
                dbContext.Orders.Add(new Db.Order() { Id = 2, OrderDate = DateTime.Now.AddDays(-6), CustomerId = 2, Total = 5500 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.ToListAsync();
                if(orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(int id)
        {
            try
            {
                var order = await dbContext.Orders.FirstOrDefaultAsync(p=>p.Id == id);
                if (order != null)
                {
                    var result = mapper.Map<Db.Order, Models.Order>(order);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
