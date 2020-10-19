using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;
using WebApplication8.Models.Home;

namespace WebApplication8.Controllers
{
    public class HomeController : Controller
    {
        EcommerceDBEntities db = new EcommerceDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel());
        }

        //public ActionResult AddToCart(int ProductID)
        //{
        //    //var cart = new List<Item>();
        //    if (Session["cart"] == null)
        //    {
        //        List<Item> cart = new List<Item>();
        //        var product = db.Products.Find(ProductID);
        //        cart.Add(new Item()
        //        {
        //            product = product,
        //            Quantity =1
        //        });
        //        Session["cart"] = cart;
        //    }
        //    else
        //    {
        //        List<Item> cart = (List<Item>)Session["cart"];
        //        var product = db.Products.Find(ProductID);
        //        cart.Add(new Item()
        //        {
        //            product = product,
        //            Quantity = 1
        //        });
        //        Session["cart"] = cart;
        //    }

        //    return Redirect("Index");
        //}

        public ActionResult AddToCart(int ProductID)
        {
            //var cart = new List<Item>();
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = db.Products.Find(ProductID);
                cart.Add(new Item()
                {
                    product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = db.Products.Find(ProductID);
                try
                {
                    foreach (Item item in (List<Item>)Session["cart"])
                    {
                        if (item.product.P_Id == ProductID)
                        {
                            //int PrevQuantity = item.Quantity;
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                product=product,
                                Quantity = item.Quantity + 1
                            });
                            

                            
                        }

                        else
                        {
                            cart.Add(new Item()
                            {
                                product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                catch
                {
                    ViewBag.Msg = "<script>alert('Error')</script>";
                }

                Session["cart"] = cart;
            }

            return Redirect("Index");
        }

        public ActionResult RemoveFromCart(int ProductID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            var product = db.Products.Find(ProductID);
            foreach (var item in cart)
            {
                if (item.product.P_Id == ProductID)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }

        public ActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(Customer data)
        {
            if (ModelState.IsValid == true)
            {
                var Record = db.Customers.Add(data);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult Cart()
        {
            TempData["Purchased"] = "<script>alert('Thankyou For Shopping')</script>";
            return View();
        }

        public ActionResult CheckOutDetails()
        {
            return View();
        }
    }
}