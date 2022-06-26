using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime Mfg_Date { get; set; }

        [DataType(DataType.Date)]
        public DateTime Exp_Date { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string ProductURL { get; set; }

        public virtual List<ProductImage> ProductImages { get; set; }

    }
}
