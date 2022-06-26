using OjayFood.Domain.Abstract;
using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EFDbContext _dbcontext;
        public EFProductRepository()
        {
            _dbcontext = new EFDbContext();
        }

        public IEnumerable<Product> GetAllProducts
        {
            get { return _dbcontext.Products; }
        }


        public IEnumerable<Product> SearchProduct(string searchTerm)
        {


            var products = _dbcontext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return products.ToList();
        }

        public void SaveProduct(Product product)
        {
            _dbcontext.Products.Add(product);

           _dbcontext.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {

            _dbcontext.Entry(product).State = System.Data.Entity.EntityState.Modified;

            _dbcontext.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {

            _dbcontext.Entry(product).State = System.Data.Entity.EntityState.Deleted;

            _dbcontext.SaveChanges();
        }

        public Product GetProductById(int Id)
        {
            return _dbcontext.Products.Find(Id);
        }

        public List<ProductImage> GetImagesByProductID(int productID)
        {
            return _dbcontext.Products.Find(productID).ProductImages.ToList();
        }

        public void SaveImageURL(string url, int Id)
        {
            var model = _dbcontext.Products.Find(Id);
            model.ProductURL = url;
            _dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
