using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Entities
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
