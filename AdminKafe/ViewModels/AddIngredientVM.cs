using AdminKafe.Date;
using AdminKafe.Models;
using AdminKafe.View.Windows;
using CV19.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdminKafe.ViewModels
{
    public class AddIngredientVM : AbstractClass<Reciep>, ICommandMethod
    {
        public AddIngredientVM()
        {
            CreateCommand = new LambdaCommand(CreateMethod, CanCloseApplicationExecat);
            DeleteCommand = new LambdaCommand(DeleteMethod, CanCloseApplicationExecat);
            EditCommand = new LambdaCommand(EditMethod, CanCloseApplicationExecat);
            SelectedEditCommand = new LambdaCommand(SelectedEditMethod, CanCloseApplicationExecat);
            ClearCommand = new LambdaCommand(ClearMethod, CanCloseApplicationExecat);
            LoadAllFood(); LoadAllProduct();
        }

       
        #region Reciep

        private string _Unit;
        public string Unit
        {
            get => _Unit;
            set => Set(ref _Unit, value);
        }
        
        private double _CountRecept;
        public double CountRecept
        {
            get => _CountRecept;
            set => Set(ref _CountRecept, value);
        }

        private Food _SelectedFood;
        public Food SelectedFood
        {
            get => _SelectedFood;
            set => Set(ref _SelectedFood, value);
        }

        private Product _SelectedProduct;
        public Product SelectedProduct
        {
            get => _SelectedProduct;
            set
            {
                Set(ref _SelectedProduct, value);
                if (_SelectedProduct != null)
                    SelectedText = _SelectedProduct.Type;
            }
        }

        private List<Food> _AllFood;
        public List<Food> AllFood
        {
            get => _AllFood;
            set=> Set(ref _AllFood, value);
               
            
        }

        private List<Product> _AllProduct;
        public List<Product> AllProduct
        {
            get => _AllProduct;
            set => Set(ref _AllProduct, value);


        }

        private string _SearcheIngridient;

        public string SearcheIngridient
        {
            get { return _SearcheIngridient; }
            set
            {
                Set(ref _SearcheIngridient, value);
                GetAllDateMethod(_SearcheIngridient);
            }
        }

        private string _SelectedTextFood;
        public string SelectedTextFood
        {
            get { return _SelectedTextFood; }
            set => Set(ref _SelectedTextFood, value);

        }

        private string _SelectedTextProduct;
        public string SelectedTextProduct
        {
            get { return _SelectedTextProduct; }
            set => Set(ref _SelectedTextProduct, value);
        }

        private string _SelectedTextGram;
        public string SelectedTextGram
        {
            get { return _SelectedTextGram; }
            set => Set(ref _SelectedTextGram, value);
        }
        #endregion Reciept

        public async void GetAllDateMethod(string name = "")
        {
            await Task.Run(() => {
                using (ApplicationContext db = new ApplicationContext())
                {
                    AllObjectRecep = DateWorker.GetAllRecipe();
                    AllObjectRecep = AllObjectRecep.Where(t => t.FoodName.Contains(name)).ToList();

                }
                //AllObjectDate = DateWorker.SearchAllIngridient(result);
                
            });
        }
        private void ClearMethod(object obj)
        {
            SelectedTextFood = null;
            SelectedTextProduct = null;
            SelectedTextGram = null;
            CountRecept = 0;
            SearcheIngridient = null;
        }
        public void CreateMethod(object p)
        {
            result = "Запольните поля ";
            if (SelectedFood == null || CountRecept == 0 || SelectedProduct == null)
            {
                result += "Названи блюда, ";

            }
            else
            {
                result = DateWorker.CreateRecieps(SelectedFood, SelectedProduct, CountRecept, Unit);
                CountRecept = 0;

            }
            OpenOkMethod(result + "!");
        }

        public void DeleteMethod(object p)
        {
            result = "";
            MessageWindow mv = new MessageWindow("Вы уеронно хотите удалить?");
            mv._mess += x =>
            {
                if (x == 1)
                {
                    //result = DateWorker.DeleteWaiter(SelectedDate);
                    LoadAllDate();
                }
            };
            mv.ShowDialog();
        }

        public void EditMethod(object p)
        {
            result = DateWorker.EditRecieps(Id, SelectedTextFood, SelectedTextProduct, SelectedTextGram, CountRecept);

            LoadAllDate();
        }

        public void SelectedEditMethod(object p)
        {
            PropertyInfo property = SelectedDateObject.GetType().GetProperty("Id");
            int Id = (int)(property.GetValue(SelectedDateObject, null));
            SelectedEdit(Id);
        }
        int Id = 0;
        public async void SelectedEdit(int tableId)
        {
            await Task.Run(() =>
            {
                try
                {
                    Id = 0;
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var result = db.Recipes.Where(t => t.Id == tableId).Select(t => new {
                            Id = t.Id,
                            FoodId = db.Foods.Where(f => f.Id == t.FoodId).Select(f => f.Name).FirstOrDefault(),
                            ProductId = db.Products.Where(f => f.Id == t.ProductId).Select(f => f.Name).FirstOrDefault(),
                            Unit = t.Unit,
                            CountPoduct = t.CountPoduct
                        }).ToList();
                        Id = result.Select(t => t.Id).FirstOrDefault();
                        SelectedTextFood = result.Select(t => t.FoodId).FirstOrDefault();
                        SelectedTextProduct = result.Select(t => t.ProductId).FirstOrDefault();
                        SelectedTextGram = result.Select(t => t.Unit).FirstOrDefault();
                        CountRecept = result.Select(t => t.CountPoduct).FirstOrDefault();
                    }
                }
                catch
                {

                }
            });

        }

        public override async void LoadAllDate(string name = "")
        {
            IsLoading = true;
            await Task.Run(() =>
            {
                //AllObjectDate = DateWorker.GetAllResiep(name);
                AllObjectRecep = DateWorker.GetAllRecipe();
            }).ContinueWith(t => IsLoading = false); 

        }

        public  async void LoadAllFood(string name = "")
        {
            await Task.Run(() =>
            {
                AllFood = DateWorker.GetAllFood();
            });

        }

        public async void LoadAllProduct(string name = "")
        {
            await Task.Run(() =>
            {
                AllProduct = DateWorker.GetAllProduct(name);
            });

        }


    }
}

