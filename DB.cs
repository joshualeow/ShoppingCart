using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart
{
    public class DB
    {
        private DBContext dbContext;

        public DB(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void Seed()
        {
        
            SeedItems();
            SeedUsersTable();
        }

        public void SeedItems()
        {
            dbContext.Add(new Item
            {
                Name = ".NET Charts",
                Price = 99,
                Category = ".NET applications",
                Description = "Brings powerful charting capabilities to your .NET applications",
                
            }) ;

            dbContext.Add(new Item
            {
                Name = ".NET PayPal",
                Price = 69,
                Category = ".NET apps",
                Description = "Integrate your .NET apps with PayPal the easy way!",
                
            });



            dbContext.SaveChanges();
        }

        public void SeedUsersTable()
        {
            HashAlgorithm sha = SHA256.Create();

            string[] usernames = { "john", "jean", "james", "kate" };

            foreach (string username in usernames)
            {
                // assuming user's password is the same as username
                // we are concatenating (i.e. username + password) to generate
                // a password hash to store in the database
                string combo = username + username;
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(combo)); //byte array is stored

                dbContext.Add(new User
                {
                    Username = username,
                    PassHash = hash
                });
            }

            dbContext.SaveChanges();
        }
    }
}
