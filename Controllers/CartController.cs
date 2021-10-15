using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        //get the database object for use in action methods
        private DBContext dbContext;
        public CartController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult ViewCart()
        {
            Session session = GetSession();
            if (session == null)
                return RedirectToAction("Index", "Logout");

            //find the cart associated to the user
            var CART = from c in dbContext.Carts
                       where c.User.Id == session.User.Id
                       select c;
            Cart cart = new Cart();
            foreach (var c in CART)
            {
                cart = c;
            }
            ViewData["cart"] = cart;
            ViewData["DataBase"] = dbContext;

            //calculate the total price in cart
            float price = (float)Math.Round(CalculatePrice(cart), 1);
            ViewData["price"] = price;

            return View();
        }

        public IActionResult CheckOut()
        {
            Session session = GetSession();
            if (session == null)
                return RedirectToAction("Index", "Logout");

            Cart cart = dbContext.Carts.FirstOrDefault(x => x.User.Id == session.User.Id);
            //check if cart is empty
            bool empty = true;
            foreach (CartItemCategory ct in cart.CartItemCategories)
            {
                if (ct.NumberOfItem > 0)
                    empty = false;
            }
            if (empty == false)
            {
                Purchase purchase = new Purchase
                {
                    PurchaseDate = DateTime.Now,
                    PurchasedItems = new List<PurchasedItem>(),
                    Userid = cart.User.Id
                };
                dbContext.Add(purchase);
                foreach (CartItemCategory ct in cart.CartItemCategories)
                {
                    for (int i = 1; i <= ct.NumberOfItem; i++)
                    {
                        PurchasedItem purchasedItem = new PurchasedItem
                        {
                            ActivationKey = Convert.ToString(new Guid()),
                            ItemId = ct.Item.Id,
                            PurchaseId = purchase.Id
                        };
                        purchase.PurchasedItems.Add(purchasedItem);
                        dbContext.Add(purchasedItem);
                        dbContext.SaveChanges();
                    }
                }

                List<CartItemCategory> ccList = dbContext.CartItemCategories.Where(x => x.Cart.Id == cart.Id).ToList();
                foreach (CartItemCategory cc in ccList)
                {
                    dbContext.Remove(cc);
                    dbContext.SaveChanges();
                }
                cart.CartItemCategories = new List<CartItemCategory>();
            }

            return RedirectToAction("MyPurchases", "Purchases");
        }

        /*
        public IActionResult AdjustNum([FromBody] Cart ct, int OPindicator)
        {
            Session session = GetSession();
            if (session == null)
                return RedirectToAction("Index", "Logout");

            Cart cart = dbContext.Carts.FirstOrDefault(x => x.User.Id == session.User.Id);
            CartItemCategory cc = dbContext.CartItemCategories.FirstOrDefault(x => x.Item.Name == _name);


            return 
        }
        */

        private float CalculatePrice(Cart cart)
        {
            float sum = 0;
            if (cart.CartItemCategories != null && cart.CartItemCategories.Count > 0)
            {
                foreach (CartItemCategory cc in cart.CartItemCategories)
                {
                    sum += cc.Item.Price * cc.NumberOfItem;
                }
            }
            return sum;
        }

        private Session GetSession()
        {
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }

            Guid sessionId = Guid.Parse(Request.Cookies["SessionId"]);

            Session session = dbContext.Sessions.FirstOrDefault(x =>
                x.Id == sessionId);

            return session;
        }
    }
}
