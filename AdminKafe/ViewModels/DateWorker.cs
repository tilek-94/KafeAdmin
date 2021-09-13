using AdminKafe.Date;
using AdminKafe.Windows.PageMenu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdminKafe.Models
{
    public class DateWorker
    {
        public static double SummByProduct = 0;
        public static double SummServices = 0;
        public static int AllId = 0;

        #region  AddProduct and ByProduct
        public static string EditProduct(string name, string type, double note)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                Product pr = db.Products.FirstOrDefault(d => d.Id == AllId);
                pr.Name = name;
                pr.Type = type;
                pr.Note = note;
                db.SaveChanges();
                result = $"Успешно изменено!";
            }
            return result;
        }
        public static int GetCountReceiptgoods(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = (from Rg in db.ReceiptGoods
                              join Pr in db.Products
                              on Rg.ProductId equals Pr.Id
                              where Pr.Name.Contains(name)
                              orderby Rg.Id descending
                              select new
                              {
                                  Id = Rg.Id
                              }).Count();

                return result;
            }

        }

        public static List<object> GetAllReceiptgoods(string name, int skipCount = 0, int takeCount = 50)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = (from Rg in db.ReceiptGoods
                              join Pr in db.Products
                              on Rg.ProductId equals Pr.Id
                              where Pr.Name.Contains(name)
                              orderby Rg.Id descending
                              select new
                              {
                                  Id = Rg.Id,
                                  ProductName = Pr.Name,
                                  DateTimeReceiptGoods = Rg.DateTimeReceiptGoods,
                                  Count = Rg.Count,
                                  Type = Pr.Type,
                                  Price = Rg.Price,
                                  Sum = Rg.Price * Rg.Count
                              }).Skip(skipCount).Take(takeCount);
                SummByProduct = result.Sum(t => t.Sum);

                return result.ToList<object>();
            }
        }

        public static string CreateReceiptGoods(DateTime dateTimeReceiptGoods, int count, double price, Product product)
        {
            string result = "Имя или Пароль уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                ReceiptGoods receiptGoods = new ReceiptGoods
                {
                    DateTimeReceiptGoods = dateTimeReceiptGoods,
                    Count = count,
                    Price = price,
                    ProductId = product.Id
                };

                db.ReceiptGoods.Add(receiptGoods);
                db.SaveChanges();

                result = "Успешно!";

                return result;
            }
        }
        public static string DeleteProduct(int id)
        {
            try
            {
                string result = "Такой комнаты не существует!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    var Id = db.ReceiptGoods.FirstOrDefault(t => t.Id == id);
                    db.ReceiptGoods.Remove(Id);
                    db.SaveChanges();

                }
                return result;
            }
            catch
            {
                return " ";
            }
        }


        public static List<Product> GetAllProduct(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Products.Where(t => t.Name.Contains(name)).OrderByDescending(t => t.Id).ToList();
                return result;
            }
        }
        public static List<object> GetAllProductSclad()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.ReceiptGoods.Join(db.Products,
                    rg => rg.ProductId,
                    p => p.Id,
                    (rg, p) => new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        DateTimeReceiptGoods = rg.DateTimeReceiptGoods,
                        Count1 = db.ReceiptGoods.Where(i => i.ProductId == rg.ProductId).Sum(i => i.Count),
                        Type = p.Type,
                        Price = rg.Price,
                        Note1 = p.Note >= db.ReceiptGoods.Where(i => i.ProductId == rg.ProductId).Sum(i => i.Count) ? 0 : 1,
                        Sum = db.ReceiptGoods.Where(i => i.ProductId == rg.ProductId).Sum(i => i.Count) * rg.Price
                    }).AsEnumerable().GroupBy(i => i.Name).Select(i => i).ToList();
                SummByProduct = result.Sum(t => t.Last().Sum);
                return result.ToList<object>();
            }
        }
        public static List<object> GetAllProductSclad1(DateTime dateDo, DateTime datePosle)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.ReceiptGoods.Join(db.Products,
                    rg => rg.ProductId,
                    p => p.Id,
                    (rg, p) => new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        DateTimeReceiptGoods = rg.DateTimeReceiptGoods,
                        Count1 = db.ReceiptGoods.Where(i => i.ProductId == rg.ProductId).Sum(i => i.Count),
                        Type = p.Type,
                        Price = rg.Price,
                        Note1 = p.Note >= db.ReceiptGoods.Where(i => i.ProductId == rg.ProductId).Sum(i => i.Count) ? 0 : 1,
                        Sum = db.ReceiptGoods.Where(i => i.ProductId == rg.ProductId).Sum(i => i.Count) * rg.Price
                    }).Where(u => u.DateTimeReceiptGoods <= datePosle && u.DateTimeReceiptGoods >= dateDo).AsEnumerable().GroupBy(i => i.Name).Select(i => i).ToList();
                SummByProduct = result.Sum(t => t.Last().Sum);
                return result.ToList<object>();
            }
        }
        public static List<object> GetAllProductSclad2(DateTime dateDo, DateTime datePosle, string name = "")
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.ReceiptGoods.Join(db.Products,
                    rg => rg.ProductId,
                    p => p.Id,
                    (rg, p) => new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        DateTimeReceiptGoods = rg.DateTimeReceiptGoods,
                        Count1 = db.ReceiptGoods.Where(i => i.ProductId == rg.ProductId).Sum(i => i.Count),
                        Type = p.Type,
                        Price = rg.Price,
                        Note1 = p.Note >= db.ReceiptGoods.Where(i => i.ProductId == rg.ProductId).Sum(i => i.Count) ? 0 : 1,
                        Sum = db.ReceiptGoods.Where(i => i.ProductId == rg.ProductId).Sum(i => i.Count) * rg.Price
                    }).Where(u => u.DateTimeReceiptGoods <= datePosle && u.DateTimeReceiptGoods >= dateDo && u.Name.Contains(name)).AsEnumerable().GroupBy(i => i.Name).Select(i => i).ToList();
                SummByProduct = result.Sum(t => t.Last().Sum);
                return result.ToList<object>();
            }
        }
        public static string CreateProduct(string name, string type, double note = 0)
        {
            string result = "Имя или Пароль уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Products.Any(el => el.Name == name);
                if (!checkIsExit)
                {
                    Product product = new Product
                    {
                        Name = name,
                        Type = type,
                        Note = note
                    };
                    db.Products.Add(product);
                    db.SaveChanges();

                    result = "Успешно!";
                }
                return result;
            }


        }

        public static string DeleteProduct(Product product)
        {
            try
            {
                string result = "Такой комнаты не существует!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                return result;
            }
            catch
            {
                return " ";
            }
        }
        public static string DeleteProductSklad()
        {
            try
            {
                string result = "Такой комнаты не существует!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    ReceiptGoods rec = db.ReceiptGoods.FirstOrDefault(u => u.Id == AllId);
                    db.ReceiptGoods.Remove(rec);
                    db.SaveChanges();
                }
                return result;
            }
            catch
            {
                return " ";
            }
        }
        public static string EditProduct(DateTime date, string massa, int count, double prise)
        {
            string result = "Такого отдела не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                ReceiptGoods waiter = db.ReceiptGoods.FirstOrDefault(d => d.Id == AllId);
                waiter.DateTimeReceiptGoods = date;
                waiter.Count = count;
                waiter.Price = prise;
                db.SaveChanges();
                result = $"Успешно изменено!";
            }
            return result;
        }
        public static List<object> GetAllProductRecipes(DateTime dateDo, DateTime datePosle, string name = "")
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = (from recipe in db.Recipes
                              join product in db.Products on recipe.ProductId equals product.Id
                              join repgods in db.ReceiptGoods on recipe.ProductId equals repgods.ProductId
                              join orders in db.Orders on recipe.FoodId equals orders.FoodId
                              join check in db.Checks on orders.CheckId equals check.Id
                              select new
                              {
                                  Id = product.Id,
                                  ProductName = product.Name,
                                  CheckDate = check.DateTimeCheck,
                                  ProductUnit = product.Type,
                                  ProductPrice = repgods.Price,
                                  ProductCount = recipe.CountPoduct * orders.CountFood,
                                  ProductSumm = repgods.Price * recipe.CountPoduct * orders.CountFood
                              }).Where(u => u.ProductName.Contains(name) && u.CheckDate > dateDo && u.CheckDate < datePosle);

                var result2 = (from s in result
                               group s by new { s.Id, s.ProductName, s.ProductUnit, s.ProductPrice, } into g
                               select new
                               {
                                   Id = g.Key.Id,
                                   ProductPrice = g.Key.ProductPrice,
                                   ProductName = g.Key.ProductName,
                                   CheckDate = dateDo.Date +" - "+ datePosle.Date,
                                   ProductCount = g.Sum(i => i.ProductCount) + " " + g.Key.ProductUnit,
                                   ProductSumm = g.Sum(i => i.ProductCount) * g.Key.ProductPrice
                               });
                SummServices = result2.Sum(p=>p.ProductSumm);
                return result2.ToList<object>();
            }
        }
        #endregion  AddProduct and ByProduct

        #region Waiter
        public static List<Waiter> GetAllWaiter(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Waiters.Where(t => t.Name.Contains(name) && t.Status==0).OrderByDescending(t => t.Id).ToList();
                return result;
            }
        }
        public static List<Waiter> GetAllWaiter1()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Waiters.Where(t => t.Status == 0).ToList();
                return result;
            }
        }
        public static List<Waiter> SearchAllWaiter(string Name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Waiters.Where(t => t.Name.Contains(Name)).ToList();
                return result;
            }
        }

        public static List<object> GetAllWaiterSalary(int id, DateTime do1, DateTime posle1)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from c in db.Checks
                             join w in db.Waiters on c.WaiterId equals w.Id
                             where c.WaiterId == id && c.DateTimeCheck >= do1 && c.DateTimeCheck <= posle1
                             select new
                             {
                                 id = c.Id,
                                 DateT = c.DateTimeCheck,
                                 Sum = w.SalaryType.Equals("Service")
                                           ? c.GuestsCount * w.Salary
                                           : w.SalaryType.Equals("Percent") ?

                                 (from o in db.Orders
                                  join f in db.Foods on o.FoodId equals f.Id
                                  where c.Id == o.CheckId
                                  select new
                                  {
                                      Food = f.Price * o.CountFood,

                                  }
                                       ).Sum(t => t.Food) * w.Salary / 100 : 0
                             };
                int i = 0;
                var result2 =
                    from f in result
                    group f by f.DateT.Date into cl
                    select new
                    {

                        DateT = cl.Key,
                        Sum = cl.Sum(t => t.Sum)
                    };
                SummServices = (from f in result
                                group f by f.DateT.Date into cl
                                select new
                                {

                                    DateT = cl.Key,
                                    Sum = cl.Sum(t => t.Sum)
                                }).Sum(t => t.Sum);
                return result2.ToList<object>();
            }
        }
        public static List<object> GetAllWaiterSalarySearch(int id, DateTime do1, DateTime posle1, DateTime search)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from c in db.Checks
                             join w in db.Waiters on c.WaiterId equals w.Id
                             where c.WaiterId == id && c.DateTimeCheck >= do1 && c.DateTimeCheck <= posle1 && c.DateTimeCheck == search
                             select new
                             {
                                 id = c.Id,
                                 DateT = c.DateTimeCheck,
                                 Sum = w.SalaryType.Equals("Service")
                                           ? c.GuestsCount * w.Salary
                                           : w.SalaryType.Equals("Percent") ?

                                 (from o in db.Orders
                                  join f in db.Foods on o.FoodId equals f.Id
                                  where c.Id == o.CheckId
                                  select new
                                  {
                                      Food = f.Price * o.CountFood,

                                  }
                                       ).Sum(t => t.Food) * w.Salary / 100 : 0
                             };
                int i = 0;
                var result2 =
                    from f in result
                    group f by f.DateT.Date into cl
                    select new
                    {

                        DateT = cl.Key,
                        Sum = cl.Sum(t => t.Sum)
                    };


                return result2.ToList<object>();
            }
        }
        public static string CreateWaiter(string name, string pass, string tel, string address, string aliasName,
            double salaryType, double salaryType2, double salaryType3)
        {
            string result = "Имя или Пароль уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                string SalaryType = "";
                double Salary = 0;
                if (salaryType > 0)
                {
                    SalaryType = "Услуга";
                    Salary = salaryType;
                }
                else if (salaryType2 > 0)
                {
                    SalaryType = "Процент";
                    Salary = salaryType2;
                }
                else if (salaryType3 > 0)
                {
                    SalaryType = "Зарплата";
                    Salary = salaryType3;
                }

                bool checkIsExit = db.Waiters.Any(el => el.Pass == pass || el.Name == name);
                if (!checkIsExit)
                {
                    Waiter waiter = new Waiter
                    {
                        Name = name,
                        Pass = pass,
                        Tel = tel,
                        address = address,
                        AliasName = aliasName,
                        SalaryType = SalaryType,
                        Salary = Salary
                    };
                    db.Waiters.Add(waiter);
                    db.SaveChanges();

                    result = "Успешно!";
                }
                return result;
            }

        }
        public static string DeleteWaiter(Waiter waiter)
        {
            try
            {
                string result = "Такого официанта нет!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    Waiter waiter1 = db.Waiters.FirstOrDefault(u => u.Id == waiter.Id);
                    waiter1.Status = 1;
                    db.SaveChanges();
                }
                return result;
            }
            catch
            {
                return " ";
            }
        }
        public static string EditWaiter(Waiter oldWaiter, string name, string pass, string tel, string address, string aliasName,
            double salaryType, double salaryType2, double salaryType3)
        {
            string result = "Такого отдела не существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                string SalaryType = "";
                double Salary = 0;
                if (salaryType > 0)
                {
                    SalaryType = "Услуга";
                    Salary = salaryType;
                }
                else if (salaryType2 > 0)
                {
                    SalaryType = "Процент";
                    Salary = salaryType2;
                }
                else if (salaryType3 > 0)
                {
                    SalaryType = "Зарплата";
                    Salary = salaryType3;
                }

                Waiter waiter = db.Waiters.FirstOrDefault(d => d.Id == oldWaiter.Id);
                waiter.Name = name;
                waiter.address = address;
                waiter.Tel = tel;
                waiter.Pass = pass;
                waiter.AliasName = aliasName;
                waiter.SalaryType = SalaryType;
                waiter.Salary = Salary;
                db.SaveChanges();
                result = $"Успешно изменено!";
            }
            return result;
        }
        #endregion Waiter

        #region TableRoom
        public static List<Location> GetAllLocation()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Locations.ToList();
                return result;
            }
        }
        public static List<object> GetAllTables()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from table in db.Tables
                             join location in db.Locations on table.LocationId equals location.Id
                             orderby location.Name
                             select new
                             {
                                 Id = table.Id,
                                 CatName = location.Name,
                                 TableName = table.Name
                             };
                return result.ToList<object>();
            }
        }
        public static List<object> GetAllTablesSearch(string searchtext = "")
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = (from table in db.Tables
                              join location in db.Locations on table.LocationId equals location.Id
                              orderby location.Name
                              select new
                              {
                                  Id = table.Id,
                                  CatName = location.Name,
                                  TableName = table.Name
                              }).Where(u => u.TableName.Contains(searchtext) || u.CatName.Contains(searchtext)).Select(u => u).ToList();
                return result.ToList<object>();
            }
        }
        public static Location GetLocationById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Location pos = db.Locations.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        public static string DeleteLocation(Location location)
        {
            try
            {
                string result = "Такой комнаты не существует!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Locations.Remove(location);
                    db.SaveChanges();

                }
                return result;
            }
            catch
            {
                return " ";
            }
        }
        public static string CreateLocation(string name)
        {
            string result = "Имя или Пароль уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Locations.Any(el => el.Name == name);
                if (!checkIsExit)
                {

                    Location location = new Location
                    {
                        Name = name
                    };
                    db.Locations.Add(location);
                    db.SaveChanges();

                    result = "Сделанно!!";
                }
                return result;
            }



        }
        public static string CreateTable(string name, Location location)
        {
            string result = "Имя или Пароль уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Tables.Any(el => el.Name == name && el.Location == location);
                if (!checkIsExit)
                {

                    Table table = new Table
                    {
                        Name = name,
                        LocationId = location.Id
                    };
                    db.Tables.Add(table);
                    db.SaveChanges();

                    result = "Успешно!";
                }
                return result;
            }

        }

        public static string DeleteTable(int tableId)
        {
            try
            {
                string result = "Такого отдела не существует!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    var tab = db.Tables.FirstOrDefault(t => t.Id == tableId);
                    db.Tables.Remove(tab);
                    db.SaveChanges();

                }
                return result;
            }
            catch
            {
                return " ";
            }
        }

        public static string EditTabel(int IdOldTable, string name, Location location)
        {
            string result = "Такого отдела не существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Table table = db.Tables.FirstOrDefault(d => d.Id == IdOldTable);
                table.Name = name;
                table.LocationId = location.Id;
                db.SaveChanges();
                result = $"Успешно изменено!";
            }
            return result;
        }
        public static string EditLocation(Location location, string name)
        {
            string result = "Такой категории не существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Location table = db.Locations.FirstOrDefault(d => d.Id == location.Id);
                table.Name = name;
                db.SaveChanges();
                result = $"Успешно изменено!";
            }
            return result;
        }
        #endregion TableRoom

        #region Foods

        public static List<Food> GetAllFoodMenu()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Foods.Where(t => t.Price == 0).OrderByDescending(t => t.Id).ToList();
                return result;
            }
        }
        public static List<Food> GetAllFood()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Foods.Where(t => t.Price > 0).OrderByDescending(t => t.Id).ToList();
                return result;
            }
        }
        public static string CreateFood(string name, double price, Food food, byte[] img)
        {
            string result = "Такое блюдо уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Foods.Any(el => el.Name == name);
                if (!checkIsExit)
                {
                    Food food1 = new Food()
                    {
                        Name = name,
                        Price = price,
                        Image = img,
                        ParentCategoryId = food.Id
                    };

                    db.Foods.Add(food1);
                    db.SaveChanges();

                    result = "Успешно!";
                }
                return result;
            }
        }
        public static string CreateMenuFood(string name, byte[] img)
        {
            string result = "Такое блюдо уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Foods.Any(el => el.Name == name);
                if (!checkIsExit)
                {
                    Food food = new Food()
                    {
                        Name = name,
                        Price = 0,
                        Image = img,
                        ParentCategoryId = 0,
                        isCook = 0
                    };

                    db.Foods.Add(food);
                    db.SaveChanges();

                    Food foodId = db.Foods.FirstOrDefault(t => t.Name == name);
                    //MessageBox.Show(Id.ToString());
                    Food f = db.Foods.FirstOrDefault(t => t.Name == name);
                    f.ParentCategoryId = foodId.Id;
                    db.SaveChanges();
                    result = "Успешно!!";
                }
            }



            return result;
        }
        public static string DeleteFood(int tableId)
        {
            try
            {
                string result = "Такого отдела не существует!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    var tab = db.Foods.FirstOrDefault(t => t.Id == tableId);
                    db.Foods.Remove(tab);
                    db.SaveChanges();

                }
                return result;
            }
            catch
            {
                return " ";
            }
        }
        public static string EditCategoriName(Food food, string name)
        {
            string result = "Выбери в первую очередь!";
            using (ApplicationContext db = new ApplicationContext())
            {
                if (food != null)
                {
                    Food food1 = db.Foods.FirstOrDefault(d => d.Id == food.Id);
                    food1.Name = name;
                    db.SaveChanges();
                    result = $"Успешно изменено!";
                }
            }
            return result;
        }

        public static string Edit(int IdFood, string Name, double Price, Food foody, int cook, byte[] img)
        {
            string result = "Успешно изменено!";
            using (ApplicationContext connetc = new ApplicationContext())
            {

                Food us = connetc.Foods.FirstOrDefault(t => t.Id == IdFood);
                us.Name = Name;
                us.Price = Price;
                us.ParentCategoryId = foody.Id;
                us.Image = img;
                us.isCook = cook;
                connetc.SaveChanges();
                return result;
            }
        }
        public static string DeleteCategoriName(Food food)
        {
            string result = "  ";
            using (ApplicationContext db = new ApplicationContext())
            {
                if (food != null)
                {
                    Food f = db.Foods.FirstOrDefault(u => u.Id == food.Id);
                    db.Foods.Remove(f);
                    db.SaveChanges();
                    result = $"Успешно изменено!";
                }
            }
            return result;
        }

        public static List<Food> SearchAllFood(string Name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Foods.Where(t => t.Name.Contains(Name)).ToList();
                return result;
            }
        }

        /*
                public static string DeleteLocation(Location location)
                {
                    try
                    {
                        string result = "Такого комната не существует!";
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            db.Locations.Remove(location);
                            db.SaveChanges();

                        }
                        return result;
                    }
                    catch
                    {
                        return " ";
                    }
                }




                }
                public static string CreateTable(string name, Location location)
                {
                    string result = "Имя или Пароль уже существует!!";
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        bool checkIsExit = db.Tables.Any(el => el.Name == name && el.Location == location);
                        if (!checkIsExit)
                        {

                            Table table = new Table
                            {
                                Name = name,
                                LocationId = location.Id
                            };
                            db.Tables.Add(table);
                            db.SaveChanges();

                            result = "Сделанно!!";
                        }
                        return result;
                    }

                }

                public static string DeleteTable(int tableId)
                {
                    try
                    {
                        string result = "Такого отдел не существует!";
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            var tab = db.Tables.FirstOrDefault(t => t.Id == tableId);
                            db.Tables.Remove(tab);
                            db.SaveChanges();

                        }
                        return result;
                    }
                    catch
                    {
                        return " ";
                    }
                }

                public static string EditTabel(int IdOldTable, string name, Location location)
                {
                    string result = "Такого отдел не существует!!";
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        Table table = db.Tables.FirstOrDefault(d => d.Id == IdOldTable);
                        table.Name = name;
                        table.LocationId = location.Id;
                        db.SaveChanges();
                        result = $"Сделанно изменение!!";
                    }
                    return result;
                }*/

        #endregion Foods

        #region Recipes
        public static List<object> GetAllResiep(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from reciep in db.Recipes
                             join food in db.Foods
                             on reciep.FoodId equals food.Id
                             join product in db.Products
                             on reciep.ProductId equals product.Id
                             where food.Name.Contains(name)
                             select new
                             {
                                 Id = reciep.Id,
                                 FoodName = food.Name,
                                 ProductName = product.Name,
                                 Unit = reciep.Unit,
                                 CountProduct = reciep.Unit == "грамм" ? reciep.CountPoduct * 1000 : reciep.CountPoduct
                             };
                return result.ToList<object>();
            }
        }
        public static List<IngridParams> GetAllRecipe()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from reciep in db.Recipes
                             join food in db.Foods
                             on reciep.FoodId equals food.Id
                             join product in db.Products
                             on reciep.ProductId equals product.Id
                             select new IngridParams
                             {
                                 Id = reciep.Id,
                                 FoodName = food.Name,
                                 ProductName = product.Name,
                                 Unit = reciep.Unit,
                                 ProductCount = reciep.Unit == "грамм" ? reciep.CountPoduct * 1000 : reciep.CountPoduct
                             };
                return result.ToList<IngridParams>();
            }
        }
        public static string CreateRecieps(Food food, Product product, double count, string unit)
        {
            string result = "Такое блюдо уже существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Recipes.Any(t => t.FoodId == food.Id && t.ProductId == product.Id && t.CountPoduct == count);
                if (!checkIsExit)
                {
                    if (unit == "грамм")
                    {
                        count /= 1000;
                    }

                    Reciep recipe = new Reciep
                    {
                        FoodId = food.Id,
                        ProductId = product.Id,
                        Unit = unit,
                        CountPoduct = count


                    };

                    db.Recipes.Add(recipe);
                    db.SaveChanges();

                    result = "Успешно сохранено!";
                }
                return result;
            }
        }
        public static string EditRecieps(int ID, string SelectedTextFood, string SelectedTextProduct, string SelectedTextGram, double CountRecept)
        {
            string result = "Успешно изменено!";
            using (ApplicationContext db = new ApplicationContext())
            {
                var res = db.Recipes.Where(t => t.Id == ID).Select(t => new
                {
                    Id = t.Id,
                    FoodId = db.Foods.Where(f => f.Name == SelectedTextFood).Select(f => f.Id).FirstOrDefault(),
                    ProductId = db.Products.Where(f => f.Name == SelectedTextProduct).Select(f => f.Id).FirstOrDefault(),
                    /* Unit = SelectedTextGram,
                     CountPoduct = CountRecept*/
                }).ToList();

                Reciep us = db.Recipes.FirstOrDefault(t => t.Id == ID);
                us.FoodId = res.Select(t => t.FoodId).FirstOrDefault();
                us.ProductId = res.Select(t => t.ProductId).FirstOrDefault();
                us.Unit = SelectedTextGram;
                us.CountPoduct = CountRecept;

                /*us.Unit = res.Select(t => t.Unit).FirstOrDefault();
                us.CountPoduct = res.Select(t => t.CountPoduct).FirstOrDefault();*/
                db.SaveChanges();
                return result;
            }

        }
        public static string DeleteRecipeTable(int Id)
        {
            
            string result = "Такого ингредиент не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                var reciep1 = db.Recipes.FirstOrDefault(u=>u.Id==Id); 
                db.Recipes.Remove(reciep1);
                db.SaveChanges();
            }
            return result;
        }
        /*public static List<IngridParams> SearchAllIngridient(string Name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = Where(t => t.Name.Contains(Name)).ToList();

                return result.ToList<IngridParams>(); ;
            }
        }*/
        #endregion

        #region Consumption
        public static List<Consumption> GetAllConsumption(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Consumptions.Where(t => t.Name.Contains(name)).ToList();
                return result;
            }
        }
        public static string CreateConsumption(string name, double summ)
        {
            string result = "Такоге блюдо уже существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Consumptions.Any(t => t.Name == name && t.Summ == summ);
                if (!checkIsExit)
                {
                    Consumption consumption = new Consumption
                    {
                        Name = name,
                        Summ = summ
                    };

                    db.Consumptions.Add(consumption);
                    db.SaveChanges();

                    result = "Успешно!";
                }
                return result;

            }
        }
        public static string DeleteConsumption(Consumption consumption)
        {
            string result = "Такого отдела не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Consumptions.Remove(consumption);
                db.SaveChanges();

            }
            return result;

        }

        public static string EditConsumption(int id, string name, double summ)
        {
            string result = "Такого отдела не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {

                Consumption cons = db.Consumptions.FirstOrDefault(t => t.Id == id);
                cons.Name = name;
                cons.Summ = summ;
                db.SaveChanges();
                result = $"Успешно изменено!";
            }
            return result;
        }

        #endregion Consumption

        #region GetAllCheck
        public static List<object> GetAllByFood()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int CounId = 0;
                var result = (from o in db.Orders
                              join f in db.Foods on o.FoodId equals f.Id
                              select new
                              {
                                  Id = CounId,
                                  FoodId = o.FoodId,
                                  Food = f.Name,
                                  CountFood = db.Orders.Where(t => t.FoodId == f.Id).Sum(t => t.CountFood),
                                  Sum = db.Orders.Where(t => t.FoodId == f.Id).Sum(t => t.CountFood) * f.Price


                              }).AsEnumerable().GroupBy(t => t.FoodId);

                return result.ToList<object>();
            }
        }


        public static (double, double, double, double) GetPP(DateTime start, DateTime end)
        {
            double pt = 0, pp = 0, ar = 0, cp = 0;
            using (ApplicationContext db = new ApplicationContext())
            {
                var consump = db.Consumptions.Select(i => i);
                double countdays = 1;
                if (end.Date != start.Date) countdays = (end - start).TotalDays;
                foreach (var item in consump)
                {
                    ar += (item.Summ * countdays);
                }
                var rs = db.HistoryChecks.Where(i => i.CheckDate.Date >= start.Date && i.CheckDate.Date <= end.Date);
                foreach (var item in rs)
                {
                    pt += returnPrice(item);
                }
                ar += (ReturnWaiters(start, end)).CostMoney;
                pp += (DoubleReturne(start, end)).CostMoney;
                cp = pt - ar - pp;
            }
            return (pt, pp, ar, cp);
        }

        private static double returnPrice(HistoryCheck item)
        { 
            using ApplicationContext db = new ApplicationContext();
            double result = 0;
            var fod = db.HistoryFoods.Where(i=>i.CheckId == item.Id);
            foreach (var ite in fod)
            {
                result += (ite.Price * ite.Count);
            }
            return result;
        }
        public static ObservableCollection<DohodClass> GetAllDohod(DateTime start, DateTime end)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = new ObservableCollection<DohodClass>();
                var consump = db.Consumptions.Select(i => i);
                double countdays = 1;
                if (end.Date != start.Date) countdays = (end - start).TotalDays;
                foreach (var item in consump)
                {
                    result.Add(new DohodClass { Name = item.Name, CostMoney = (item.Summ * countdays) });
                }
                result.Add(ReturnWaiters(start, end));
                result.Add(DoubleReturne(start, end));
                return new ObservableCollection<DohodClass>(result);
            }
        }

        private static DohodClass ReturnWaiters(DateTime start, DateTime end)
        {
            var result = new DohodClass
            {
                Name = "Официант",
                CostMoney = 0.0
            };
            using (ApplicationContext db = new ApplicationContext())
            {
                var historychecks = db.HistoryChecks.Where(i => i.SalaryType != "Salary" && i.CheckDate.Date >= start.Date && i.CheckDate.Date <= end.Date).Select(i => i);
                foreach (var item in historychecks)
                {
                    if (item.SalaryType == "Percent")
                    {
                        result.CostMoney += ReturnSalary(item);
                    }
                    else
                    {
                        result.CostMoney += item.Salary * item.GuestCount;
                    }
                }

                var salaryMoney = from hc in db.HistoryChecks.Where(i => i.SalaryType == "Salary" && i.CheckDate.Date >= start.Date && i.CheckDate.Date <= end.Date)
                                  group hc by new { hc.WaiterName, hc.Salary } into g
                                  select new
                                  {
                                      Name = g.Key.WaiterName,
                                      Price = g.Key.Salary
                                  };
                foreach (var item in salaryMoney)
                {

                    result.CostMoney += item.Price / 30;
                }
            }
            return result;
        }
        public static DohodClass DoubleReturne(DateTime dateDo, DateTime datePosle)
        {
            var resull = new DohodClass
            {
                Name = "Продукты",
                CostMoney = 0
            };
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = (from recipe in db.Recipes
                              join product in db.Products on recipe.ProductId equals product.Id
                              join repgods in db.ReceiptGoods on recipe.ProductId equals repgods.ProductId
                              join orders in db.Orders on recipe.FoodId equals orders.FoodId
                              join check in db.Checks on orders.CheckId equals check.Id
                              select new
                              {
                                  Id = product.Id,
                                  ProductName = product.Name,
                                  CheckDate = check.DateTimeCheck,
                                  ProductUnit = product.Type,
                                  ProductPrice = repgods.Price,
                                  ProductCount = recipe.CountPoduct * orders.CountFood,
                                  ProductSumm = repgods.Price * recipe.CountPoduct * orders.CountFood
                              }).Where(u => u.CheckDate.Date >= dateDo.Date && u.CheckDate.Date <= datePosle.Date);

                var result2 = (from s in result
                               group s by new { s.Id, s.ProductName, s.ProductUnit, s.ProductPrice, } into g
                               select new
                               {
                                   Id = g.Key.Id,
                                   ProductPrice = g.Key.ProductPrice,
                                   ProductName = g.Key.ProductName,
                                   CheckDate = dateDo.Date + " - " + datePosle.Date,
                                   ProductCount = g.Sum(i => i.ProductCount) + " " + g.Key.ProductUnit,
                                   ProductSumm = g.Sum(i => i.ProductCount) * g.Key.ProductPrice
                               });
                resull.CostMoney = result2.Sum(p => p.ProductSumm);
                return resull;
            }
        }
        private static double ReturnSalary(HistoryCheck id)
        {
            double returnedDouble = 0.0;
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.HistoryFoods.Where(i => i.CheckId == id.Id).Select(i => i);
                foreach (var item in result)
                {
                    returnedDouble += (item.Count * item.Price);
                }

            }
            return (returnedDouble * id.Salary / 100);
        }
        public static List<CheckFoodClass> GetAllOrder(int id, int GoustCount)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from o in db.Orders
                             join f in db.Foods on o.FoodId equals f.Id
                             join c in db.Checks on o.CheckId equals c.Id
                             join w in db.Waiters on c.WaiterId equals w.Id
                             where (o.CheckId == id)
                             select new CheckFoodClass
                             {
                                 Id = o.Id,
                                 Food = f.Name,
                                 Check = c.CheckCount,
                                 CoundFood = o.CountFood,
                                 Price = f.Price,
                                 Summ = Convert.ToDouble(o.CountFood) * f.Price,
                                 SalaryType = w.SalaryType,
                                 SalarySumm = w.Salary,
                                 //Service=


                             };
                double a = result.Sum(t => t.Summ);
                var Summ = from s in result
                           select new
                           {
                               Summ = s.SalaryType.Equals("Service") ? GoustCount * s.SalarySumm : s.SalaryType.Equals("Percent") ? SummByProduct * s.SalarySumm / 100 : 0
                           };
                SummServices = Summ.Select(t => t.Summ).FirstOrDefault();
                SummByProduct = a + SummServices;
                return result.ToList<CheckFoodClass>();
            }
        }


        /*        public static List<object> GetAll()
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var result = from check in db.Checks
                                     join table in db.Tables on check.TableId equals table.Id
                                     join waiter in db.Waiters on check.WaiterId equals waiter.Id
                                     join status in db.Status on check.Status equals status.Id
                                     join loc  in db.Locations on table.LocationId equals loc.Id
                                     select new
                                     {
                                         Id = check.Id,
                                         CheckCount = check.CheckCount,
                                         CheckDate = check.DateTimeCheck,
                                         TableCategoryName = loc.Name,
                                         TableName = table.Name,
                                         WaiterName = waiter.Name,
                                         Status = status.Name,
                                         GuestCount = check.GuestsCount

                                     };


                        return result.ToList<object>();
                    }
                }*/


        public static double countReturn(int id)
        {
            double returndouble = 0.0;
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Orders.Where(i => i.CheckId == id);
                foreach (var item in result)
                {
                    returndouble += item.CountFood * countReturnfoof(item.FoodId); //
                }
                return returndouble;
            }
        }

        public static double countReturnfoof(int FoodId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Foods.Where(i => i.Id == FoodId).Select(i => i.Price).OrderBy(i => i).LastOrDefault();
            }
        }


        public static List<CheckFoodName> GetAllCkeck(DateTime DateTimeForOneDay, DateTime DateTimeForTwoDay, DateTime DateTimeForTreeDay, string WaiterName = "")
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from check in db.Checks
                             join table in db.Tables on check.TableId equals table.Id
                             join waiter in db.Waiters on check.WaiterId equals waiter.Id
                             join status in db.Status on check.Status equals status.Id
                             join loc in db.Locations on table.LocationId equals loc.Id
                             /*where check.DateTimeCheck.Date == DateTimeForOneDay.Date*/
                             select new CheckFoodName
                             {
                                 Id = check.Id,
                                 CheckCount = check.CheckCount,
                                 CheckDate = check.DateTimeCheck,
                                 TableName = table.Name,
                                 TableCategoryName = loc.Name,
                                 WaiterName = waiter.Name,
                                 GuestCount = check.GuestsCount,
                                 Status = status.Name,
                                 StatusID = status.Id,
                                 CheckSumm = countReturn(check.Id) + (waiter.SalaryType.Equals("Service") ? check.GuestsCount * waiter.Salary : waiter.SalaryType.Equals("Percent") ? SummByProduct * waiter.Salary / 100 : 0),
                             };

                if (AllReport.StatusTrue == 0)
                {
                    if (AllReport.RadioCheck == 0)
                    {
                        if (AllReport.ComboChek == 0)
                            return result.ToList<CheckFoodName>();
                        if (AllReport.ComboChek == 1)
                            return result.Where(i => i.WaiterName == WaiterName).ToList<CheckFoodName>();
                    }
                    if (AllReport.RadioCheck == 1)
                    {
                        if (AllReport.ComboChek == 0)
                            return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date).ToList<CheckFoodName>();
                        if (AllReport.ComboChek == 1)
                            return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date && i.WaiterName == WaiterName).ToList<CheckFoodName>();
                    }
                    if (AllReport.RadioCheck == 2)
                    {
                        if (AllReport.ComboChek == 0)
                        {
                            return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date).ToList<CheckFoodName>();
                        }
                        if (AllReport.ComboChek == 1)
                        {
                            return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date && i.WaiterName == WaiterName).ToList<CheckFoodName>();
                        }
                    }
                }
                if (AllReport.StatusTrue == 1)
                {
                    if (AllReport.RadioCheck == 0)
                    {
                        if (AllReport.ComboChek == 0)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.ToList<CheckFoodName>();
                            else
                                return result.Where(i => i.StatusID == AllReport.RadioCheckStatus).ToList<CheckFoodName>();
                        }
                        if (AllReport.ComboChek == 1)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.WaiterName == WaiterName).ToList<CheckFoodName>();
                            else
                                return result.Where(i => i.WaiterName == WaiterName && i.StatusID == AllReport.RadioCheckStatus).ToList<CheckFoodName>();
                        }
                    }
                    if (AllReport.RadioCheck == 1)
                    {
                        if (AllReport.ComboChek == 0)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date).ToList<CheckFoodName>();
                            else
                                return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date && i.StatusID == AllReport.RadioCheckStatus).ToList<CheckFoodName>();
                        }
                        if (AllReport.ComboChek == 1)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date && i.WaiterName == WaiterName).ToList<CheckFoodName>();
                            else
                                return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date && i.WaiterName == WaiterName && i.StatusID == AllReport.RadioCheckStatus).ToList<CheckFoodName>();
                        }
                    }
                    if (AllReport.RadioCheck == 2)
                    {
                        if (AllReport.ComboChek == 0)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date).ToList<CheckFoodName>();
                            else
                                return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date && i.StatusID == AllReport.RadioCheckStatus).ToList<CheckFoodName>();
                        }
                        if (AllReport.ComboChek == 1)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date).ToList<CheckFoodName>();
                            else
                                return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date && i.WaiterName == WaiterName && i.StatusID == AllReport.RadioCheckStatus).ToList<CheckFoodName>();
                        }
                    }
                }
                return result.ToList<CheckFoodName>();

            }
        }

        public static List<string> GetAllWaiters()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                var result = db.Waiters.Select(w => w.Name);
                return result.ToList<string>();

            }
        }

        public static ObservableCollection<CoolGet> GetAllWastedFood(DateTime second, DateTime first, string searchtext = "")
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var checks = db.HistoryChecks.Where(i => i.CheckDate <= first && i.CheckDate >= second).Join(db.HistoryFoods,
                    hc => hc.Id,
                    hf => hf.CheckId,
                    (hc, hf) => new CoolGet
                    {
                        FoodName = hf.Name,
                        FoodCount = hf.Count,
                        FoodPrice = hf.Price,
                        Price = hf.Price,
                        Gram = hf.Gram
                    });

                var result = (from g in checks
                              group g by new { g.FoodName, g.Gram, g.FoodPrice,g.Price} into res
                              select new CoolGet
                              {
                                  FoodName = res.Key.FoodName,
                                  FoodCount = res.Sum(i => i.FoodCount),
                                  Price=res.Key.Price,
                                  FoodPrice = (res.Sum(i => i.FoodCount) * res.Key.FoodPrice)
                              }).Where(u => u.FoodName.Contains(searchtext));
                SummByProduct = result.Sum(u=>u.FoodPrice);
                return new ObservableCollection<CoolGet>(result);
            }
        }

        /*ublic static Location GetLocationById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Location pos = db.Locations.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        public static string DeleteLocation(Location location)
        {
            try
            {
                string result = "Такого комната не существует!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Locations.Remove(location);
                    db.SaveChanges();

                }
                return result;
            }
            catch
            {
                return " ";
            }
        }
        public static string CreateLocation(string name)
        {
            string result = "Имя или Пароль уже существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Locations.Any(el => el.Name == name);
                if (!checkIsExit)
                {

                    Location location = new Location
                    {
                        Name = name
                    };
                    db.Locations.Add(location);
                    db.SaveChanges();

                    result = "Сделанно!!";
                }
                return result;
            }



        }
        public static string CreateTable(string name, Location location)
        {
            string result = "Имя или Пароль уже существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Tables.Any(el => el.Name == name && el.Location == location);
                if (!checkIsExit)
                {

                    Table table = new Table
                    {
                        Name = name,
                        LocationId = location.Id
                    };
                    db.Tables.Add(table);
                    db.SaveChanges();

                    result = "Сделанно!!";
                }
                return result;
            }

        }

        public static string DeleteTable(int tableId)
        {
            try
            {
                string result = "Такого отдел не существует!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    var tab = db.Tables.FirstOrDefault(t => t.Id == tableId);
                    db.Tables.Remove(tab);
                    db.SaveChanges();

                }
                return result;
            }
            catch
            {
                return " ";
            }
        }

        public static string EditTabel(int IdOldTable, string name, Location location)
        {
            string result = "Такого отдел не существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Table table = db.Tables.FirstOrDefault(d => d.Id == IdOldTable);
                table.Name = name;
                table.LocationId = location.Id;
                db.SaveChanges();
                result = $"Сделанно изменение!!";
            }
            return result;
        }*/
        #endregion TableRoom
    }
}