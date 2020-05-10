namespace eCommerce.Api.Customers.Providers
{
    #region <Usings>
    using AutoMapper;
    using eCommerce.Api.Customers.Db;
    using eCommerce.Api.Customers.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    #endregion

    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Customers.Any() || dbContext.Customers.Count() == 0)
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Microsoft", Address = "Seattle, USA" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Coca Cola", Address = "Atlanta, Georgia, USA" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Mercedez Benz", Address = "Dunwoody, Georgia, USA" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Boing", Address = "Seattle, USA" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if(customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
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

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(p=>p.Id == id);
                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
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
