using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OjayFood.Areas.Admin.Models
{
    public class PaymentTypeViewModel
    {
        public IEnumerable<PaymentType> PaymentTypes { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Phone { get; set; }

        public string CardNumber { get; set; }

        public string Description { get; set; }
    }

    public class PaymentTypeActionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}