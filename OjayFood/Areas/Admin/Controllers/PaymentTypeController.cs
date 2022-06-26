using OjayFood.Areas.Admin.Models;
using OjayFood.Domain.Abstract;
using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OjayFood.Areas.Admin.Controllers
{
    public class PaymentTypeController : Controller
    {
        PaymentTypeController() { }

        private IPaymentTypeRepository _payTypeRepo;

        public PaymentTypeController(IPaymentTypeRepository repository)
        {
            this._payTypeRepo = repository;
        }

        // GET: Admin/PaymentType
        public ActionResult Index()
        {
            PaymentTypeViewModel model = new PaymentTypeViewModel();

            model.PaymentTypes = _payTypeRepo.GetAllPaymentTypes;

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new PaymentType());
        }

        [HttpPost]
        public ActionResult Create(PaymentType paymentType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _payTypeRepo.SavePaymentType(paymentType);

                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            PaymentType paymentType = _payTypeRepo.GetPaymentType(id);
            return View(paymentType);
        }

        [HttpPost]
        public ActionResult Edit(PaymentType paymentType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _payTypeRepo.UpdatePaymentType(paymentType);

                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(paymentType);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {

            PaymentTypeActionModel model = new PaymentTypeActionModel();

            var paymentType = _payTypeRepo.GetPaymentType(ID);

            model.Id = paymentType.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(PaymentTypeActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var paymentType = _payTypeRepo.GetPaymentType(model.Id);

            _payTypeRepo.DeletePaymentType(paymentType.Id);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Department." };
            }

            return json;
        }

    }
}