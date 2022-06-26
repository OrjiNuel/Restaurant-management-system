using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OjayFood.Areas.Admin.Models
{
    public class ImageViewModel
    {
        public IEnumerable<Image> Images { get; set; }
    }
}