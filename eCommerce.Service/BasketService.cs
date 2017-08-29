using eCommerce.Contracts.Repositories;
using eCommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eCommerce.Service
{
    public class BasketService
    {
        IRepositoryBase<Basket> baskets;
        private IRepositoryBase<Voucher> vouchers;
        private IRepositoryBase<VoucherType> voucherTypes;
        private IRepositoryBase<BasketVoucher> basketVouchers;

        public const string BasketSessionName = "eCommerceBasket";


        public BasketService(IRepositoryBase<Basket> baskets, IRepositoryBase<Voucher> vouchers, IRepositoryBase<BasketVoucher> basketVouchers, IRepositoryBase<VoucherType> voucherType)
        {
            this.baskets = baskets;
            this.vouchers = vouchers;
            this.basketVouchers = basketVouchers;
        }

        private Basket createNewBasket(HttpContextBase httpContext)
        {
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            Basket basket = new Basket();
            basket.date = DateTime.Now;
            //basket.BasketId = Guid.NewGuid();

            baskets.Insert(basket);
            baskets.Commit();

            cookie.Value = basket.BasketId.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }


        public bool AddToBasket(HttpContextBase httpContext, int productId, int quantity)
        {
            bool success = true;

            Basket basket = GetBasket(httpContext);

            //Get item with id
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            //Check if item alread exist with id
            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.BasketId,
                    //BasketId = Guid.NewGuid(),
                    ProductId = productId,
                    Quantity = quantity
                };
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + quantity;
            }

            baskets.Commit();

            return success;
        }

        public Basket GetBasket(HttpContextBase httpContext)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            Basket basket;

            Guid basketId;

            if (cookie != null)
            {

                if (Guid.TryParse(cookie.Value, out basketId))
                {
                    basket = baskets.GetById(basketId);
                }
                else
                {
                    //Basket is expire
                    basket = createNewBasket(httpContext);
                }
            }
            else
            {
                basket = createNewBasket(httpContext);
            }

            return basket;
        }
        public void AddVoucher(string voucherCode,HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext);
            Voucher voucher = vouchers.GetAll().FirstOrDefault(v => v.VoucherCode == voucherCode);
            if (voucher != null)
            {
                VoucherType voucherType = voucherTypes.GetById(voucher.VoucherTypeId);
                if(voucherType != null)
                {
                    BasketVoucher basketVoucher = new BasketVoucher();
                    if (voucherType.Type == "MoneyOff")
                    {
                        MoneyOff(voucher, basket, basketVoucher);
                    }
                    if (voucherType.Type == "PercentOff")
                    {
                       // PercentOff(voucher, basket, basketVoucher);
                        
                    }
                    baskets.Commit();
                }

            }
           
        }
        public void MoneyOff(Voucher voucher, Basket basket, BasketVoucher basketVoucher)
        {
            basketVoucher.Value = voucher.Value * -1;
            basketVoucher.VoucherCode = voucher.VoucherCode;
            basketVoucher.VoucherDescription = voucher.VoucherDescription;
            basketVoucher.VoucherId = voucher.VoucherId;
            //basket.A
        }
    }
}
