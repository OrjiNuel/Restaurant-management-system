using OjayFood.Areas.Admin.Models;
using OjayFood.Domain.Abstract;
using OjayFood.Domain.Entities;
using OjayFood.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;

namespace OjayFood.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _prodRepo;
        private IOrderProcessor orderProcessor;

        public HomeController(IProductRepository productRepository, IOrderProcessor proc)
        {
            this._prodRepo = productRepository;
            this.orderProcessor = proc;
            
        }


        public ActionResult Index()
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = _prodRepo.GetAllProducts
            };
            
            return View(model);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = _prodRepo.GetProductById(productId);
                foreach (var item in cart)
                {
                    if (item.Product.Id == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }

        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = _prodRepo.GetProductById(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = _prodRepo.GetProductById(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.Id == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.Id == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Index");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.Id == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }

        public ViewResult Delivery()
        {

            //var w = Session["cart"];

            int Total = 0;

            foreach(Item item in (List<Item>)Session["cart"])
            {
                int lineTotal = Convert.ToInt32(item.Quantity * item.Product.Price);
                Total = Convert.ToInt32(Total + lineTotal);
            }
            ViewBag.Total = Total;
            

            return View(new ShippingDetail());
        }

        [HttpPost]
        public ViewResult Delivery(Cart cart, ShippingDetail shippingDetail)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetail);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetail);
            }
        }

        public ActionResult Drink()
        {

            return View();
        }

      

        public ActionResult Completed()
        {

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}