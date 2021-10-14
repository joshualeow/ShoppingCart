using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class PurchasesController : Controller
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            private DBContext dbContext;

            public PurchasesController(DBContext dbContext)
            {
                this.dbContext = dbContext;
            }
            public IActionResult Index()
            {
                Session session = GetSession();
                if (session == null)
                {
                    return RedirectToAction("Index", "Logout");
                }
                List<ShoppingCart.Models.Item> items = dbContext.Items.ToList();
                ViewData["items"] = items;

            List<ShoppingCart.Models.PurchasedItem> pitemslist = dbContext.PurchasedItems.ToList();
            
            



            return View();
            }

            private Session GetSession()
            {
                if (Request.Cookies["SessionId"] == null)
                {
                    return null;
                }

                Guid sessionId = Guid.Parse(Request.Cookies["SessionId"]);// checking for the cookie,
                                                                          // extract session and pass back
                Session session = dbContext.Sessions.FirstOrDefault(x =>
                    x.Id == sessionId
                );

                return session;//to use user information
            }

            private string GetPurchaseId([FromBody] Purchase purchase)
            {
                string purid = purchase.Id.ToString();
                List<PurchasedItem> itemslist = dbContext.PurchasedItems.ToList();
                IEnumerable<string> pur = 
                    from i in itemslist
                    select i.PurchaseId.ToString();

                List<string> idlist = pur.ToList();
                var returns = pur.Any(id => id == purid);
                if (returns)
                {
                    string purID = purid;
                    return purID;
                }
                return purid;
            //Purchase purchase = dbContext.PurchasedItems.FirstOrDefault(x => x.Id == pur);

            }
=======
        private DBContext dbContext;
        public PurchasesController([FromServices] DBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult MyPurchases()
        {
=======
        private DBContext dbContext;
        public PurchasesController([FromServices] DBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult MyPurchases()
        {
>>>>>>> Stashed changes
            // redirect back to login page if session has expired or doesn't exist
            Session session = GetSession();
            if (session == null)
                return RedirectToAction("Index", "Login");
            // get all data:
            // item data from Items table,
            // Activation Key from PurchaseItems, and PurchaseDate from Purchases
            var query = dbContext.Purchases
                .Where(x => x.Customer.id == session.User.id)
                .Join(dbContext.PurchasedItems,
                      purchase => purchase.Id, pItem => pItem.PurchaseId,
                      (purchase, pItem) => new
                      {
                          ActivationKey = pItem.ActivationKey,
                          ItemId = pItem.ItemId,
                          PurchaseDate = purchase.PurchaseDate
                      })
                .Join(dbContext.Items,
                      pJoin => pJoin.ItemId, item => item.Id,
                      (pJoin, item) => new
                      {
                          ItemID = item.Id,
                          ActivationKey = pJoin.ActivationKey,
                          PurchaseDate = pJoin.PurchaseDate,
                          Name = item.Name,
                          Description = item.Description,
                          ImageURL = item.ImageUrl
                      })
                .OrderByDescending(x => x.PurchaseDate);
            ViewData["query"] = query;
            return View();
        }
        private Session GetSession()
        {
            if(Request.Cookies["SessionID"] == null)
                return null;
            Guid sessionID = Guid.Parse(Request.Cookies["SessionID"]);
            Session session = dbContext.Sessions.FirstOrDefault(x => x.Id == sessionID);
            return session;
        }
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    }
}

