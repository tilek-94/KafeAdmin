using AdminKafe.Date;
using AdminKafe.Windows.PageMenu;
using System;
using System.Collections.Generic;
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
                result = $"Сделанно изменение!!";
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
            string result = "Имя или Пароль уже существует!!";
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

                result = "Сделанно!!";

                return result;
            }
        }
        public static string DeleteProduct(int id)
        {
            try
            {
                string result = "Такого комната не существует!";
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
            string result = "Имя или Пароль уже существует!!";
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

                    result = "Сделанно!!";
                }
                return result;
            }


        }

        public static string DeleteProduct(Product product)
        {
            try
            {
                string result = "Такого комната не существует!";
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
                string result = "Такого комната не существует!";
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
            string result = "Такого отдел не существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                ReceiptGoods waiter = db.ReceiptGoods.FirstOrDefault(d => d.Id == AllId);
                waiter.DateTimeReceiptGoods = date;
                waiter.Count = count;
                waiter.Price = prise;
                db.SaveChanges();
                result = $"Сделанно изменение!!";
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
                                  CheckDate = check.DateTimeCheck.Date,
                                  ProductUnit = product.Type,
                                  ProductPrice = repgods.Price,
                                  ProductCount = recipe.CountPoduct * orders.CountFood,
                                  ProductSumm = repgods.Price * recipe.CountPoduct * orders.CountFood
                              });

                var result2 = (from s in result
                              group s by new { s.Id, s.ProductName, s.CheckDate, s.ProductUnit, s.ProductPrice } into g
                              select new
                              {
                                  Id = g.Key.Id,
                                  CheckDate = g.Key.CheckDate,
                                  ProductPrice = g.Key.ProductPrice,
                                  ProductName = g.Key.ProductName,
                                  ProductCount = g.Sum(i => i.ProductCount) + " " + g.Key.ProductUnit,
                                  ProductSumm = g.Sum(i => i.ProductCount) * g.Key.ProductPrice
                              }).Where(u =>/* u.CheckDate <= datePosle.Date && u.CheckDate >= dateDo.Date && */u.ProductName.Contains(name));
                SummServices = result2.Sum(u => u.ProductSumm);
                return result2.ToList<object>();
            }
        }
        #endregion  AddProduct and ByProduct

        #region Waiter
        public static List<Waiter> GetAllWaiter(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Waiters.Where(t => t.Name.Contains(name)).OrderByDescending(t => t.Id).ToList();
                return result;
            }
        }
        public static List<Waiter> GetAllWaiter1()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Waiters.ToList();
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
            string result = "Имя или Пароль уже существует!!";
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

                    result = "Сделанно!!";
                }
                return result;
            }

        }
        public static string DeleteWaiter(Waiter waiter)
        {
            try
            {
                string result = "Такого отдел не существует!";
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Waiters.Remove(waiter);
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
            string result = "Такого отдел не существует!!";
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
                result = $"Сделанно изменение!!";
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
                              }).Where(u => u.TableName.Contains(searchtext) || u.CatName.Contains(searchtext)).Select(u=>u).ToList();
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
        }
        public static string EditLocation(Location location,string name)
        {
            string result = "Такого категория не существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Location table = db.Locations.FirstOrDefault(d => d.Id == location.Id);
                table.Name = name;
                db.SaveChanges();
                result = $"Сделанно изменение!!";
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
        public static  List<Food> GetAllFood()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Foods.Where(t => t.Price > 0).OrderByDescending(t => t.Id).ToList();
                return result;
            }
        }
        public static string CreateFood(string name, double price, Food food, byte[] img)
        {
            string result = "Такого блюда уже существует!!";
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

                    result = "Сделанно!!";
                }
                return result;
            }
        }
        public static string CreateMenuFood(string name, byte[] img)
        {
            string result = "Такого блюда уже существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Foods.Any(el => el.Name == name);
                if (!checkIsExit)
                {
                    Food food = new Food()
                    {
                        Name = name,
                        Price = 0,
                        Image=img,
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
                    result = "Сделанно!!";
                }
            }



            return result;
        }
        public static string DeleteFood(int tableId)
        {
            try
            {
                string result = "Такого отдел не существует!";
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
                result = $"Сделанно изменение!!";
                }
            }
            return result;
        }

        public static string DeleteCategoriName(Food food)
        {
            string result = "  ";
            using (ApplicationContext db = new ApplicationContext())
            {
                if (food != null)
                {
                    Food f = db.Foods.FirstOrDefault(u=>u.Id==food.Id);
                    db.Foods.Remove(f);
                    db.SaveChanges();
                    result = $"Сделанно изменение!!";
                }
            }
            return result;
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
        public static string CreateRecieps(Food food, Product product, double count, string unit)
        {
            string result = "Такого блюда уже существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExit = db.Recipes.Any(t => t.FoodId == food.Id && t.ProductId == product.Id && t.CountPoduct == count);
                if (!checkIsExit)
                {
                    if (unit == "грамм")
                    {
                        count = count / 1000;
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

                    result = "Сделанно!!";
                }
                return result;
            }
        }


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
            string result = "Такого блюда уже существует!!";
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

                    result = "Сделанно!!";
                }
                return result;

            }
        }
        public static string DeleteConsumption(Consumption consumption)
        {
            string result = "Такого отдел не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Consumptions.Remove(consumption);
                db.SaveChanges();

            }
            return result;

        }

        public static string EditConsumption(int id, string name, double summ)
        {
            string result = "Такого отдел не существует!!";
            using (ApplicationContext db = new ApplicationContext())
            {

                Consumption cons = db.Consumptions.FirstOrDefault(t => t.Id == id);
                cons.Name = name;
                cons.Summ = summ;
                db.SaveChanges();
                result = $"Сделанно изменение!!";
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
                                  Sum= db.Orders.Where(t => t.FoodId == f.Id).Sum(t => t.CountFood)*f.Price


                              }).AsEnumerable().GroupBy(t=>t.FoodId);
              
                return result.ToList<object>();
            }
        }
        public static List<object> GetAllOrder(int id, int GoustCount)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from o in db.Orders
                             join f in db.Foods on o.FoodId equals f.Id
                             join c in db.Checks on o.CheckId equals c.Id
                             join w in db.Waiters on c.WaiterId equals w.Id
                             where (o.CheckId == id)
                             select new
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
                return result.ToList<object>();
            }
        }

       
        public static List<object> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from check in db.Checks
                             join table in db.Tables on check.TableId equals table.Id
                             join waiter in db.Waiters on check.WaiterId equals waiter.Id
                             join status in db.Status on check.Status equals status.Id
                             select new
                             {
                                 Id = check.Id,
                                 CheckCount = check.CheckCount,
                                 CheckDate = check.DateTimeCheck,
                                 TableName = table.Name,
                                 WaiterName = waiter.Name,
                                 Status = status.Name,
                                 GuestCount = check.GuestsCount

                             };


                return result.ToList<object>();
            }
        }


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


        public static List<object> GetAllCkeck(DateTime DateTimeForOneDay, DateTime DateTimeForTwoDay, DateTime DateTimeForTreeDay, string WaiterName = "")
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = from check in db.Checks
                             join table in db.Tables on check.TableId equals table.Id
                             join waiter in db.Waiters on check.WaiterId equals waiter.Id
                             join status in db.Status on check.Status equals status.Id
                             /*where check.DateTimeCheck.Date == DateTimeForOneDay.Date*/
                             select new
                             {
                                 Id = check.Id,
                                 CheckCount = check.CheckCount,
                                 CheckDate = check.DateTimeCheck,
                                 TableName = table.Name,
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
                            return result.ToList<object>();
                        if (AllReport.ComboChek == 1)
                            return result.Where(i => i.WaiterName == WaiterName).ToList<object>();
                    }
                    if (AllReport.RadioCheck == 1)
                    {
                        if (AllReport.ComboChek == 0)
                            return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date).ToList<object>();
                        if (AllReport.ComboChek == 1)
                            return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date && i.WaiterName == WaiterName).ToList<object>();
                    }
                    if (AllReport.RadioCheck == 2)
                    {
                        if (AllReport.ComboChek == 0)
                        {
                            return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date).ToList<object>();
                        }
                        if (AllReport.ComboChek == 1)
                        {
                            return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date && i.WaiterName == WaiterName).ToList<object>();
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
                                return result.ToList<object>();
                            else
                                return result.Where(i => i.StatusID == AllReport.RadioCheckStatus).ToList<object>();
                        }
                        if (AllReport.ComboChek == 1)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.WaiterName == WaiterName).ToList<object>();
                            else
                                return result.Where(i => i.WaiterName == WaiterName && i.StatusID == AllReport.RadioCheckStatus).ToList<object>();
                        }
                    }
                    if (AllReport.RadioCheck == 1)
                    {
                        if (AllReport.ComboChek == 0)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date).ToList<object>();
                            else
                                return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date && i.StatusID == AllReport.RadioCheckStatus).ToList<object>();
                        }
                        if (AllReport.ComboChek == 1)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date && i.WaiterName == WaiterName).ToList<object>();
                            else
                                return result.Where(i => i.CheckDate.Date == DateTimeForOneDay.Date && i.WaiterName == WaiterName && i.StatusID == AllReport.RadioCheckStatus).ToList<object>();
                        }
                    }
                    if (AllReport.RadioCheck == 2)
                    {
                        if (AllReport.ComboChek == 0)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date).ToList<object>();
                            else
                                return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date && i.StatusID == AllReport.RadioCheckStatus).ToList<object>();
                        }
                        if (AllReport.ComboChek == 1)
                        {
                            if (AllReport.RadioCheckStatus == 0)
                                return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date).ToList<object>();
                            else
                                return result.Where(i => i.CheckDate.Date >= DateTimeForTwoDay.Date && i.CheckDate.Date <= DateTimeForTreeDay.Date && i.WaiterName == WaiterName && i.StatusID == AllReport.RadioCheckStatus).ToList<object>();
                        }
                    }
                }
                return result.ToList<object>();

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