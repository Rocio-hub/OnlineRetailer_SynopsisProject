﻿using System;
namespace OrderApi.Models
{
    public class BECustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public double CreditStanding { get; set; }
    }
}
