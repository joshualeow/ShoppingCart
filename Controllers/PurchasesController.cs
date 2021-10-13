using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class PurchasesController : Controller
    {
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
    }
}

