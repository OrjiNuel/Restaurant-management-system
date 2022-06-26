using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Concrete
{
    public class EFImageRepository
    {
        private readonly EFDbContext _dbcontext;
        public EFImageRepository()
        {
            _dbcontext = new EFDbContext();
        }

        public IEnumerable<Image> GetAllImages
        {
            get { return _dbcontext.Images; }
        }

        public bool SaveImage(Image image)
        {
            _dbcontext.Images.Add(image);

            return _dbcontext.SaveChanges() > 0;
        }

        public IEnumerable<Image> GetImagesByIDs(List<int> imageIDs)
        {
            return imageIDs.Select(x => _dbcontext.Images.Find(x)).ToList();
        }
    }
}
