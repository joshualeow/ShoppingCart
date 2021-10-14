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
        private DBContext dbContext;
        public PurchasesController([FromServices] DBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult MyPurchases()
        {
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
    }
}
