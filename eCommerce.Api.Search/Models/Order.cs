﻿using System;
using System.Collections.Generic;

namespace eCommerce.Api.Search.Models
{
    public class Order
    {

        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
