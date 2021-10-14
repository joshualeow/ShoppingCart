using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class GalleryController : Controller
    {
        private DBContext dbContext;

        public GalleryController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult AllProducts(string searchStr)
        {
            Session session = GetSession();
            if (session == null)
            {
                return RedirectToAction("Index", "Logout");
            }

            List<ShoppingCart.Models.Item> items = dbContext.Items.ToList();
            //if(searchStr == null)
            //{
            //    searchStr = "";
            //}

            //List<Item> items = dbContext.Items.Where(x => x.Name.Contains(searchStr)).ToList();

            //ViewData["searchStr"] = searchStr;
            //ViewData["items"] = items;

            ViewData["items"] = items;

            return View();
        }

        public IActionResult Search(string searchStr)
        {
            if (searchStr == null)
            {
                searchStr = "";
                return RedirectToAction("AllProducts");
            }

            List<Item> items = dbContext.Items.Where(x =>
                x.Name.Contains(searchStr) ||
                x.Description.Contains(searchStr)
            ).ToList();

            ViewData["searchStr"] = searchStr;
            ViewData["items"] = items;
            return View();
        }

        public JsonResult AutoComplete(string searchterm)
        {
            var products = (from items in dbContext.Items
                            where items.Name.Contains(searchterm)
                            select new
                            {
                                label = items.Name,
                                val = items.Id
                            }).ToList();

            return Json(products);
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

        //public IActionResult AddItem([FromBody] Item items) //This will be able to accept the list of taskId
        //{
        //    Session session = GetSession();
        //    if (session == null)
        //    {
        //        return Json(new { status = "fail" });
        //    }

        //    /* everything okay so far; proceed */
        //    foreach (string id in items.Id)
        //    {
        //        Guid taskId = Guid.Parse(id);
        //        TasksCart.Models.Task task = dbContext.Tasks.FirstOrDefault(x => x.Id == taskId);
        //        task.ReserveTime = null;
        //        task.User = session.User;
        //    }
        //    dbContext.SaveChanges();

        //    return Json(new { status = "success" });
        //}
    }
}