using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        
        public PaymentType PaymentTypeId { get; set; }

        [DataType("decimal(16, 8)")]
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Phone { get; set; }

        public string CardNumber { get; set; }

        public string Description { get; set; }
    }
}
