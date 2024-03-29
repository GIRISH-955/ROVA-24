using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Net;
using System;
using Microsoft.AspNetCore.Identity;
namespace ROVA_24.Model
{
    public class Customer
    {
        
    public int CustomerId {  get; set; }
        public String Name { get; set; }
        public String email {  get; set; }
        public String Password {  get; set; }
        public bool Enable { get; set; } = true;
        public String PhoneNumber { get; set; }
        public List<Address> Addresses {  get; set; }
        public List<Order> Orders {  get; set; }
        public List<Review> Reviews {  get; set; }
    }

}

