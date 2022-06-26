using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts { get; }
        IEnumerable<Product> SearchProduct(string searchTerm);
        void SaveImageURL(string url, int Id);
        List<ProductImage> GetImagesByProductID(int productID);
        void SaveProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Product GetProductById(int Id);
        void Dispose();
    }
}
