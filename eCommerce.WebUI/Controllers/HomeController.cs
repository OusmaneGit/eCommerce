using eCommerce.Contracts.Repositories;
using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using eCommerce.Model;
using eCommerce.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
        IRepositoryBase<Basket> baskets;
        IRepositoryBase<Voucher> vouchers;
        IRepositoryBase<BasketVoucher> basketVouchers;
        IRepositoryBase<VoucherType> voucherType;
        BasketService basketService;
        public HomeController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products, IRepositoryBase<Basket> baskets
            , IRepositoryBase<Voucher> vouchers, IRepositoryBase<BasketVoucher> basketVouchers, IRepositoryBase<VoucherType> voucherType)
        {
            this.customers = customers;
            this.products = products;
            this.baskets = baskets;
            this.vouchers = vouchers;
            this.basketVouchers = basketVouchers;
            this.voucherType = voucherType;
            basketService = new BasketService(this.baskets);
        }

        public ActionResult BasketSummary()
        {
            var model = basketService.GetBasket(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(int id)
        {
            basketService.AddToBasket(this.HttpContext, id, 1);//always add one to ther basket
            return RedirectToAction("BasketSummary");
        }
        public ActionResult Index()
        {
            //CustomerRepository customers = new CustomerRepository(new DataContext());
            //CustomerRepository products = new CustomerRepository(new DataContext());
            //ProductRepository products = new ProductRepository(new DataContext());
            //new CustomerRepository(new DataContext());
            var productList = products.GetAll();

            return View(productList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Details(int id)
        {
            var product = products.GetById(id);

            return View(product);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}