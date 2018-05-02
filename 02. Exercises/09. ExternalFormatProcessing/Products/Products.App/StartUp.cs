using Newtonsoft.Json;
using Products.Data;
using Products.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Products.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            ResetDatabase();
            Create();
            ImportJson();
            ImportXml();
            ExportJson();
            ExportXml();
        }
        private static void Create()
        {
            if (!Directory.Exists("Results"))
            {
                Directory.CreateDirectory("Results");

                if (!Directory.Exists(@"Results\Json"))
                {
                    Directory.CreateDirectory(@"Results\Json");
                }
                if (!Directory.Exists(@"Results\Xml"))
                {
                    Directory.CreateDirectory(@"Results\Xml");
                }
            }
        }

        private static void ResetDatabase()
        {
            using (var db = new ProductContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }

        private static void ImportXml()
        {
            ImportUsersXml();
            ImportCategoriesXml();
            ImportProductsXml();
        }

        private static void ExportXml()
        {
            ExportProductsInRangeXml();
            ExportCategoriesByProductsCountXml();
            ExportUsersAndProductsXml();
            ExportSoldProductsXml();
        }

        private static void ImportJson()
        {
            ImportUsersJson();
            ImportCategoriesJson();
            ImportProductsJson();
        }

        private static void ExportJson()
        {
            ExportProductsInRangeJson();
            ExportSuccessfullySoldProductsJson();
            ExportCategoriesByProductsCountJson();
            ExportUsersAndProductsJson();
        }
                                              //JSON
                                 //I. IMPORT
        //1.    Import Users JSON
        private static void ImportUsersJson()
        {
            var users = File.ReadAllText("Sources/Json/users.json");
            var usersFromJson = JsonConvert.DeserializeObject<User[]>(users);
            using (ProductContext db = new ProductContext())
            {
                db.Users.AddRange(usersFromJson);
                db.SaveChanges();
            }
        }

        //2.    Import Categories JSON
        private static void ImportCategoriesJson()
        {
            var categories = File.ReadAllText("Sources/Json/categories.json");
            var categoriesFromJson = JsonConvert.DeserializeObject<Category[]>(categories);
            Random rnd = new Random();

            using (ProductContext db = new ProductContext())
            {
                db.Categories.AddRange(categoriesFromJson);
                db.SaveChanges();
                var productIds = db.Products.Select(p => p.Id).ToArray();
                var categoryIds = db.Categories.Select(c => c.Id).ToArray();
                List<ProductCategory> prods = new List<ProductCategory>();
                foreach (var pcId in productIds)
                {
                    var i = rnd.Next(0, categoryIds.Length);
                    var categoryId = categoryIds[i];
                    ProductCategory pc = new ProductCategory()
                    {
                        ProductId = pcId,
                        CategoryId = categoryId
                    };
                    prods.Add(pc);

                }
                db.ProductsCategories.AddRange(prods);
                db.SaveChanges();
            }
        }
         
        //3.    Import Products JSON
        private static void ImportProductsJson()
        {
            var products = File.ReadAllText("Sources/Json/products.json");
            var productsFromJson = JsonConvert.DeserializeObject<Product[]>(products);
            using (ProductContext db = new ProductContext())
            {
                //var prods = db.Products.Select(p => p.Id).ToArray();
                //List<Product> productsList = new List<Product>();
                var sellers = db.Users.Select(u => u.UserId).ToArray();
                var buyers = db.Users.Select(u => u.UserId).ToArray();
                Random rndSell = new Random();
                Random rndBuy = new Random();
                foreach (var prod in productsFromJson)
                {
                    var sellIndex = rndSell.Next(0, sellers.Length);
                    var buyIndex = rndBuy.Next(0, buyers.Length/3);
                    prod.SellerId = sellers[sellIndex];
                    prod.BuyerId = buyers[buyIndex];
                }
                db.Products.AddRange(productsFromJson);
                db.SaveChanges();
            }
        }
                                 //II. EXPORT
        //4.    Export Products in Range JSON
        private static void ExportProductsInRangeJson()
        {
            using (ProductContext db = new ProductContext())
            {
                var products = db.Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        seller = $"{p.Seller.FirstName} {p.Seller.LastName }"
                    }).ToList();
                var productsToJson = JsonConvert.SerializeObject(products,Formatting.Indented);
                File.WriteAllText(@"Results\Json\01.Products-in-range.json", productsToJson);
            }
        }

        //5.    Export Successfully sold Products JSON
        private static void ExportSuccessfullySoldProductsJson()
        {
            using (ProductContext db = new ProductContext())
            {
                var users = db.Users
                    .Include(u => u.ProductsSold)
                    .ThenInclude(p => p.Buyer)
                    .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                    .OrderByDescending(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        soldProducts = u.ProductsSold.Where(p => p.Buyer != null)
                                        .Select(ps => new
                                        {
                                            name = ps.Name,
                                            price = ps.Price,
                                            buyerFirstName = ps.Buyer.FirstName,
                                            buyerLastName = ps.Buyer.LastName
                                        })
                    }).ToList();
                var usersExport = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(@"Results\Json\02.Users-sold-products.json", usersExport);
            }
        }

        //6.    Export Categories by Products Count JSON
        private static void ExportCategoriesByProductsCountJson()
        {
            using (ProductContext db = new ProductContext())
            {
                var categories = db.ProductsCategories
                        .Include(cp => cp.Category)
                        .Include(cp => cp.Product)
                .Where(c => c.Category.Products.Count > 0)
                .OrderByDescending(c => c.Category.Name)
                .Select(c => new
                {
                    category = c.Category.Name,
                    productCount = c.Category.Products.Count(),
                    averagePrice = c.Category.Products.Select(cp => cp.Product.Price).Average(),
                    totalRevenue = c.Category.Products.Where(cp => cp.Product.BuyerId != null).Select(cp => cp.Product.Price).Sum()
                })
                .ToArray();
                var cats = JsonConvert.SerializeObject(categories, Formatting.Indented);
                File.WriteAllText(@"Results\Json\03.Categories-by-products.json", cats);
            }
        }

        //7.    Export Users and Products JSON
        private static void ExportUsersAndProductsJson()
        {
            using (ProductContext db = new ProductContext())
            {
                var users = db.Users
                        .Include(u => u.ProductsSold)
                        .ThenInclude(p => p.Buyer)
                        .Where(u => u.ProductsSold.Count(p => p.Buyer != null) >= 1)
                        .ToArray();
                var jsonUsers = new
                {
                    usersCount = users.Length,
                    users = users.Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new
                        {
                            count = u.ProductsSold.Count(p => p.Buyer != null),
                            products = u.ProductsSold.Where(p => p.Buyer != null)
                            .Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            }).ToArray()
                        }
                    }).ToArray()
                    .OrderByDescending(upr => upr.soldProducts.count)
                    .ThenBy(ur => ur.lastName)
                };
                var usersJson = JsonConvert.SerializeObject(jsonUsers, Formatting.Indented);
                File.WriteAllText(@"Results\Json\04.Users-and-products.json", usersJson);
            }
        }


                                                        //XML
                                    //I. IMPORT
        //08.   Import Users XML
        private static void ImportUsersXml()
        {
            //XDocument document = XDocument.Load("Sources\\Xml\\users.xml");
            XDocument document = XDocument.Load(@"E:\Coding\03_ProfModules\C#\DB\Advanced\Ex\09. ExternalFormatProcessing\Products\Products.App\Sources\Xml\categories.xml");
            
            var users = document.Root.Elements();
            using (ProductContext db = new ProductContext())
            {
                List<User> xmlToDb = new List<User>();
                foreach (var user in users)
                {
                    var firstname = user.Attribute("firstName")?.Value;
                    var lastName = user.Attribute("lastName")?.Value?? "Koko";
                    var age = user.Attribute("age")?.Value;
                    int? ageParsed = int.TryParse(age, out int result) ? result : default(int?);

                    User currUser = new User()
                    {
                        FirstName = firstname,
                        LastName = lastName,
                        Age = ageParsed
                    };
                    xmlToDb.Add(currUser);
                }
                db.Users.AddRange(xmlToDb);
                db.SaveChanges();
            }
        }

        //09.   Import Categories XML
        private static void ImportCategoriesXml()
        {
            using (ProductContext db = new ProductContext())
            {
                XDocument document = XDocument.Load(@"E:\Coding\03_ProfModules\C#\DB\Advanced\Ex\09. ExternalFormatProcessing\Products\Products.App\Sources\Xml\categories.xml");
                List<Category> xmlToDb = new List<Category>();
                var categories = document.Root.Elements();
                Random rndCat = new Random();
                foreach (var category in categories)
                {
                    var name = category.Element("name")?.Value;
                    var currentCategory = new Category()
                    {
                        Name = name
                    };

                    xmlToDb.Add(currentCategory);
                }

                db.Categories.AddRange(xmlToDb);
                db.SaveChanges();

                var categoryIds = db.Categories.Select(c => c.Id).ToArray();
                var productIds = db.Products.Select(p => p.Id).ToArray();

                var catProds = new List<ProductCategory>();

                foreach (var pId in productIds)
                {
                    var i = rndCat.Next(0, categoryIds.Length);
                    var categoryId = categoryIds[i];

                    var currentCategoryProduct = new ProductCategory()
                    {
                        ProductId = pId,
                        CategoryId = categoryId
                    };

                    catProds.Add(currentCategoryProduct);

                }

                db.ProductsCategories.AddRange(catProds);
                db.SaveChanges();
            }
        }

        //10.   Import Products XML
        private static void ImportProductsXml()
        {
            using (ProductContext db = new ProductContext())
            {
                XDocument document = XDocument.Load(@"E:\Coding\03_ProfModules\C#\DB\Advanced\Ex\09. ExternalFormatProcessing\Products\Products.App\Sources\Xml\products.xml");
                var products = document.Root.Elements();
                List<Product> xmlToDb = new List<Product>();
                var sellers = db.Users.Select(u => u.UserId).ToArray();
                var buyers = db.Users.Select(u => u.UserId).ToArray();
                Random rndbuy = new Random();
                Random rndSell = new Random();
                foreach (var product in products)
                {
                    var sellIndex = rndSell.Next(0, sellers.Length);
                    var buyIndex = rndbuy.Next(0, buyers.Length / 3);
                    var name = product.Element("name").Value;
                    var price = decimal.Parse(product.Element("price").Value);

                    var currProd = new Product()
                    {
                        Name = name,
                        Price = price
                    };
                    currProd.SellerId = sellers[sellIndex];
                    currProd.BuyerId = buyers[buyIndex];
                    xmlToDb.Add(currProd);
                }
                db.Products.AddRange(xmlToDb);
                db.SaveChanges();
            }
        }

                                            //II. EXPORT

        //11.   Export Products in Range XML
        private static void ExportProductsInRangeXml()
        {
            using (ProductContext db = new ProductContext())
            {
                var products = db.Products
                        .Include(p => p.Buyer)
                        .Where(p => p.Price <= 2000 && p.Price >= 1000 && p.Buyer != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                        })
                        .OrderBy(p => p.price).ToArray();
                XDocument document = new XDocument();
                document.Add(
                    new XElement("products"));

                foreach (var product in products)
                {
                    document.Root.Add(
                        new XElement("product",
                        new XAttribute("name", $"{product.name}"),
                        new XAttribute("price", $"{product.price}"),
                        new XAttribute("buyer", $"{product.buyer}")));
                }
                document.Save("Results/Xml/01.Products-in-range.xml");
                //string path = "Results/Xml/01.ProductsInRange.xml";
                //document.Save(path);
            }
        }

        //12.   Export Successfully sold Products XML
        private static void ExportSoldProductsXml()
        {
            using (ProductContext db = new ProductContext())
            {
                var users = db.Users
                    .Include(u => u.ProductsSold)
                    .ThenInclude(p => p.Buyer)
                    .Where(u => u.ProductsSold.Count(p => p.Buyer != null) >= 1)
                    .Select(u => new
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        Products = u.ProductsSold.Where(p => p.Buyer != null)
                            .Select(p => new
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                            .ToArray()
                    })
                    .OrderBy(u => u.FirstName)
                    .ThenBy(u => u.LastName)
                    .ToArray();

                
                var document = new XDocument(new XElement("users"));
                foreach (var u in users)
                {
                    var userInfo = new XElement("user",
                        new XAttribute("first-name", u.FirstName ?? "No name"),
                        new XAttribute("last-name", u.LastName),
                        new XAttribute("age", u.Age.ToString() ?? "No age"),
                        new XElement("sold-products"));

                    foreach (var shit in u.Products)
                    {
                        var products = new XElement("product",
                        new XAttribute("name", shit.Name),
                        new XAttribute("price", shit.Price));

                        userInfo.Element("sold-products").Add(products);
                    }

                    document.Root.Add(userInfo);

                }






                //foreach (var user in users)
                //{
                //    document.Root.Add(
                //        new XElement("user",
                //            new XAttribute("first-name", $"{user.FirstName}"),
                //            new XAttribute("last-name", $"{user.LastName}"),
                //                new XElement("sold-products")));

                //    var productElements = document.Root.Elements()
                //        .SingleOrDefault(e => e.Name == "user"
                //                              && e.Attribute("first-name").Value == $"{user.FirstName}"
                //                              && e.Attribute("last-name").Value == $"{user.LastName}")
                //                                .Element("sold-products");

                //    foreach (var p in user.Products)
                //    {
                //        productElements
                //            .Add(new XElement("product",
                //                     new XElement("name", $"${p.Name}"),
                //                        new XElement("price", $"{p.Price}")));
                //    }
                //}
                //string path = @"Results/Xml/02.Users-sold-products.xml";
                //document.Save(path);
                document.Save("Results/Xml/02.Users-sold-products.xml");
                //document.Save(@"Results/02.Users-sold-products.xml");
            }
        }

        //13.   Export Categories by Products Count XML
        private static void ExportCategoriesByProductsCountXml()
        {
            using (ProductContext db = new ProductContext())
            {
                var categories = db.ProductsCategories
                        .Include(cp => cp.Category)
                        .Include(cp => cp.Product)
                    .Select(c => new
                    {
                        category = c.Category.Name,
                        productCount = c.Category.Products.Count(),
                        averagePrice = c.Category.Products.Select(cp => cp.Product.Price).Average(),
                        totalRevenue = c.Category.Products.Where(cp => cp.Product.BuyerId != null).Select(cp => cp.Product.Price).Sum()
                    })
                    .OrderByDescending(c => c.productCount)
                    .ToArray();

                XDocument document = new XDocument();
                document.Add(new XElement("categories"));

                foreach (var c in categories)
                {
                    document.Root.Add(new XElement("category",
                                        new XAttribute("name", $"{c.category}"),
                                            new XElement("products-count", $"{c.productCount}"),
                                            new XElement("average-price", $"{c.averagePrice}"),
                                            new XElement("total-revenue", $"{c.totalRevenue}")));
                }
                //string path = @"Results\Xml\03.Categories-by-products.xml";
                //document.Save(path);
                document.Save("Results/Xml/03.Categories-by-products.xml");
                //document.Save(@"03.Categories-by-products.xml");
            }
        }
        //14.   Export Users and Products XML
        private static void ExportUsersAndProductsXml()
        {
            using (ProductContext db = new ProductContext())
            {
                var users = db.Users
                        .Include(u => u.ProductsSold)
                        .ThenInclude(p => p.Buyer)
                        .Where(u => u.ProductsSold.Count(p => p.Buyer != null) >= 1)
                        .ToArray();
                var xmlUsers = new
                {
                    usersCount = users.Length,
                    users = users.Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new
                        {
                            count = u.ProductsSold.Count(p => p.Buyer != null),
                            products = u.ProductsSold.Where(p => p.Buyer != null)
                            .Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            }).ToArray()
                        }
                    }).ToArray()
                    .OrderByDescending(upr => upr.soldProducts.count)
                    .ThenBy(ur => ur.lastName)
                };

                var document = new XDocument(new XElement("users", new XAttribute("count", users.Count())));
                foreach (var u in users)
                {
                    var userInfo = new XElement("user",
                        new XAttribute("first-name", u.FirstName ?? "No name"),
                        new XAttribute("last-name", u.LastName),
                        new XAttribute("age", u.Age.ToString() ?? "No age"),
                        new XElement("sold-products",
                        new XAttribute("count", u.ProductsSold.Count)));

                    foreach (var shit in u.ProductsSold)
                    {
                        var products = new XElement("product",
                        new XAttribute("name", shit.Name),
                        new XAttribute("price", shit.Price));

                        userInfo.Element("sold-products").Add(products);
                    }

                    document.Root.Add(userInfo);

                }





                //XDocument document = new XDocument();
                //document.Add(new XElement("users", new XAttribute("count", $"{xmlUsers.usersCount}")));

                //foreach (var u in xmlUsers.users)
                //{
                //    document.Root.Add(new XElement("user",
                //        new XAttribute("first-name", $"{u.firstName}"),
                //        new XAttribute("last-name", $"{u.lastName}"),
                //        new XAttribute("age", $"{u.age}"),
                //            new XElement("sold-products",
                //            new XAttribute("count", $"{u.soldProducts.count}"))));

                //    var element =
                //        document.Root.Elements()
                //        .SingleOrDefault(e => e.Name == "user"
                //        && e.Attribute("first-name").Value == $"{u.firstName}"
                //        && e.Attribute("last-name").Value == $"{u.lastName}"
                //        && e.Attribute("age").Value == $"{u.age}")
                //        .Elements()
                //        .SingleOrDefault(e => e.Name == "sold-products");

                //    foreach (var p in u.soldProducts.products)
                //    {
                //        element.Add(new XElement("product",
                //                        new XAttribute("name", $"{p.name}"),
                //                        new XAttribute("price", $"{p.price}")));
                //    }
            //}
                //string path = "Results/Xml/04.Usersandproducts.xml";
                //document.Save(path);
                document.Save("Results/Xml/04.Users-and-products.xml");
                //document.Save(@"Results04.Users-and-products.xml");
            }
        }
    }
}
