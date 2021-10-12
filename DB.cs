using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
                Category = ".NET Applications",
                Description = "Brings powerful charting capabilities to your .NET applications",
                ImageUrl = "\\images\\NETCharts.jpg"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Numerics",
                Price = 199,
                Category = ".NET Simulations",
                Description = "Powerful and efficient numerical methods for your .NET simulations",
                ImageUrl = "\\images\\NETNumerics.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Paypal",
                Price = 69,
                Category = ".NET API",
                Description = "Integrate your .NET apps with paypal the easy way all your convenience",
                ImageUrl = "\\images\\NETPaypal.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET ML",
                Price = 299,
                Category = ".NET Libraries",
                Description = "Supercharged.Net machine learning libraries",
                ImageUrl = "\\images\\NETML.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Analytics",
                Price = 299,
                Category = ".NET Libraries",
                Description = "Performs data mining and analytics easilly in .NET",
                ImageUrl = "\\images\\NETAnalytics.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Logger",
                Price = 69,
                Category = ".NET API",
                Description = "Logs and aggregates events easily in your .NET apps",
                ImageUrl = "\\images\\NETLogger.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET NUnit",
                Price = 39,
                Category = ".NET Framework",
                Description = "An open source unit testing framework for .NET, written in C# and thus cross-platform.",
                ImageUrl = "\\images\\NETNunit.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Entity Framework",
                Price = 69,
                Category = ".NET Framework",
                Description = "An open source object–relational mapping (ORM) framework for ADO.NET",
                ImageUrl = "\\images\\NETEntity.jpg"
            });

            dbContext.Add(new Item
            {
                Name = ".NET ASP.NET Core",
                Price = 39,
                Category = ".NET Framework",
                Description = "A successor and re-implementation of ASP.NET as a modular web framework",
                ImageUrl = "\\images\\NETAsp.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Blazor",
                Price = 39,
                Category = ".NET Framework",
                Description = "A free and open-source web framework that enables developers to create Web apps",
                ImageUrl = "\\images\\NETBlazor.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Meta.Numerics",
                Price = 69,
                Category = ".NET Simulations",
                Description = "Meta.Numerics is a library for advanced scientific computation in the .NET Framework.",
                ImageUrl = "\\images\\NETMeta.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET ALGLIB",
                Price = 69,
                Category = ".NET Simulations",
                Description = "ALGLIB is a cross-platform open source numerical analysis and data processing library",
                ImageUrl = "\\images\\NETALGLIB.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET AForge",
                Price = 39,
                Category = ".NET Libraries",
                Description = "AForge is a computer vision and artificial intelligence library.  It implements a number of image processing algorithms and filters .",
                ImageUrl = "\\images\\NETAForge.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET React.NET",
                Price = 99,
                Category = ".NET Simulations",
                Description = "React.NET is a open source library for programmatically developing discrete event simulations implemented on top of the .NET Framework.",
                ImageUrl = "\\images\\NETReact.PNG"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Meadow",
                Price = 199,
                Category = ".NET Applications",
                Description = "Meadow by Wilderness Labs gives you .NET on embedded devices and allows you to build production-grade and powerful solutions using .NET",
                ImageUrl = "\\images\\NETMeadow.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Xamarin",
                Price = 199,
                Category = ".NET Applications",
                Description = "Xamarin is a set of tools and libraries for building cross-platform apps on the .NET platform",
                ImageUrl = "\\images\\NETXamarin.png"
            });

            dbContext.Add(new Item
            {
                Name = ".NET Google Maps",
                Price = 69,
                Category = ".NET API",
                Description = "Integrate your .NET apps with Google Maps to search any locations in ASP.NET",
                ImageUrl = "\\images\\NETMap.png"
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

        private string CreateActivationKey()
        {
            var activationKey = Guid.NewGuid().ToString();

            List<PurchasedItem> item = dbContext.PurchasedItems.Where(x => x.ActivationKey == x.ActivationKey).ToList();
            IEnumerable<string> iter =
                from i in item
                select i.ActivationKey;

            List<string> keylist = iter.ToList();

            var exists = keylist.Any(key => key == activationKey);

            if (exists)
            {
                activationKey = CreateActivationKey();
            }

            return activationKey;
        }
    }
}