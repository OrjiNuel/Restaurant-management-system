using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Entities
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public Product ProductId { get; set; }
        public int Quantity { get; set; }
        public string Stock_Detail { get; set; }
    }
}

