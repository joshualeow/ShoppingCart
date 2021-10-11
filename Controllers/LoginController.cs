﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        private DBContext dbContext;

        public LoginController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult LoginIndex()
        {
            if (Request.Cookies["SessionId"] != null)
            { //Check for cookies straight away, if there is existing
                Guid sessionId = Guid.Parse(Request.Cookies["sessionId"]); //request from cookies
                Session session = dbContext.Sessions.FirstOrDefault(x =>
                    x.Id == sessionId //calidate against database, If cannot find 
                );

                if (session == null)
                {
                    // invalid Session ID; route to Logout, someone trying to fake userid
                    return RedirectToAction("Index", "Logout"); //Cannot find in session table, Logout
                }

                // valid Session ID; route to landing page
                return RedirectToAction("AllProducts", "Gallery"); //Action method and controller
            }

            // no Session ID; show Login page
            return View();
        }
        public IActionResult Login(IFormCollection form) //IFormCollection is imported from AspNetCore.Http
        {
            string username = form["username"];
            string password = form["password"];

            HashAlgorithm sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(username + password));

            User user = dbContext.Users.FirstOrDefault(x => 
                x.Username == username && 
                x.PassHash == hash
            );
            //Cart cart = dbContext.Cart.FirstOrDefault(x => x.Quantity > 0);

            if(user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Session session = new Session()
            {
                User = user
            };

            dbContext.Sessions.Add(session);
            dbContext.SaveChanges();

            Response.Cookies.Append("SessionId", session.Id.ToString()); //options can include expire date and path
            Response.Cookies.Append("Username", user.Username);

            return RedirectToAction("AllProducts", "Gallery");
        }
    }
}