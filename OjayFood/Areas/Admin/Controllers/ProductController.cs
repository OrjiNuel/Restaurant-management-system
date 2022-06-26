using System;
using OjayFood.Domain.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjayFood.Areas.Admin.Models;
using OjayFood.Domain.Abstract;
using OjayFood.Domain.Entities;
using OjayFood.Models;

namespace OjayFood.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
    
    public ProductController() { }
        
        private IProductRepository _prodRepo;
        public int PageSize = 5;

        public ProductController(IProductRepository productRepository)
        {
            this._prodRepo = productRepository;
        }


        public ActionResult Index(string category, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = _prodRepo.GetAllProducts
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),


                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        _prodRepo.GetAllProducts.Count() :
                        _prodRepo.GetAllProducts.Where(e => e.Category == category).Count()
                },
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? Id)
        {
            ProductActionViewModel model = new ProductActionViewModel();

            if (Id.HasValue) //we are trying to edit a record
            {
                var product = _prodRepo.GetProductById(Id.Value);

                model.Id = product.Id;
                model.Name = product.Name;
                model.Price = product.Price;
                model.Mfg_Date = product.Mfg_Date;
                model.Exp_Date = product.Exp_Date;
                model.Category = product.Category;
                model.Description = product.Description;

                model.ProductImages = _prodRepo.GetImagesByProductID(product.Id);
            }
            
            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(ProductActionViewModel model)
        {

            JsonResult json = new JsonResult();

            var result = false;
            var efImageRepo = new EFImageRepository();
            var list = model.ImageIDs;
            var imgIDs = string.Join(",", list);

            List<int> imageIDs = !string.IsNullOrEmpty(imgIDs) ? model.ImageIDs.Select(x => int.Parse(x)).ToList() : new List<int>();

            var images = efImageRepo.GetImagesByIDs(imageIDs);

            if (model.Id > 0) //we are trying to edit a record
            {
                var product = _prodRepo.GetProductById(model.Id);

                product.Id = model.Id;
                product.Name = model.Name;
                product.Price = model.Price;
                product.Mfg_Date = model.Mfg_Date;
                product.Exp_Date = model.Exp_Date;
                product.Category = model.Category;
                product.Description = model.Description;
                product.ProductURL = images.Select(x => x.URL).FirstOrDefault();

                product.ProductImages.AddRange(images.Select(x => new ProductImage() { ProductID = product.Id, ImageID = x.ID }));
                
                _prodRepo.UpdateProduct(product);
            }
            else //we are trying to create a record
            {
                Product product = new Product();

                //product.Id = model.Id;
                product.Name = model.Name;
                product.Price = model.Price;
                product.Mfg_Date = model.Mfg_Date;
                product.Exp_Date = model.Exp_Date;
                product.Category = model.Category;
                product.Description = model.Description;
                product.ProductURL = images.Select(x => x.URL).FirstOrDefault();


                product.ProductImages = new List<ProductImage>();
                product.ProductImages.AddRange(images.Select(x => new ProductImage() { ProductID = product.Id, ImageID = x.ID }));

                _prodRepo.SaveProduct(product);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Products." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {

            ProductActionViewModel model = new ProductActionViewModel();

            var product = _prodRepo.GetProductById(Id);

            model.Id = product.Id;

            return PartialView("_Delete", model);
        }


        [HttpPost]
        public JsonResult Delete(ProductActionViewModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var product = _prodRepo.GetProductById(model.Id);

            _prodRepo.DeleteProduct(product);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Product." };
            }

            return json;
        }
    }
}