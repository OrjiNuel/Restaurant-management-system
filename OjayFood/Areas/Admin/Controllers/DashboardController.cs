using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OjayFood.Areas.Admin.Models;
using OjayFood.Domain.Concrete;
using OjayFood.Domain.Entities;

namespace OjayFood.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        EFImageRepository efImageRepo = new EFImageRepository();

        public ActionResult Index()
        {
            ImageViewModel model = new ImageViewModel
            {
                Images = efImageRepo.GetAllImages
            };
            return View();
        }

        [HttpPost]
        public JsonResult UploadImages()
        {
            JsonResult result = new JsonResult();

            var imgList = new List<Image>();



            var files = Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                var image = files[i];

                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Server.MapPath("~/Images/Site/") + fileName; 

                image.SaveAs(filePath);

                var dbImage = new Image();
                dbImage.URL = fileName;

                if (efImageRepo.SaveImage(dbImage))
                {
                    imgList.Add(dbImage);
                }
            }

            result.Data = imgList;

            return result;
        }
    }
}