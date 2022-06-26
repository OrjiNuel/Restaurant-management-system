using OjayFood.Areas.Admin.Models;
using OjayFood.Domain.Abstract;
using OjayFood.Domain.Entities;
using OjayFood.Models.Home;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjayFood.Controllers
{
    public class PaymentController : Controller
    {
        private IPaymentTypeRepository _payType;
        private IPaymentRepository _paymentRepo;

        public PaymentController(IPaymentTypeRepository payTypeRepo, IPaymentRepository payRepo)
        {
            this._payType = payTypeRepo;
            this._paymentRepo = payRepo;
        }
        // GET: Payment
        public ActionResult Index()
        {

            int Total = 0;

            foreach (Item item in (List<Item>)Session["cart"])
            {
                int lineTotal = Convert.ToInt32(item.Quantity * item.Product.Price);
                Total = Convert.ToInt32(Total + lineTotal);
            }
            ViewBag.Total = Total;

            PaymentTypeViewModel model = new PaymentTypeViewModel();

            model.PaymentTypes = _payType.GetAllPaymentTypes;
            

            return View(model);
        }

        public ActionResult Add()
        {
            PaymentTypeViewModel model = new PaymentTypeViewModel();

            int Total = 0;

            foreach (Item item in (List<Item>)Session["cart"])
            {
                int lineTotal = Convert.ToInt32(item.Quantity * item.Product.Price);
                Total = Convert.ToInt32(Total + lineTotal);
            }
            ViewBag.Total = Total;

            return View(model);
        }
        [HttpPost]
        public ActionResult Add(Payment payment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _paymentRepo.SavePayment(payment);

                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(payment);
        }
    }
}