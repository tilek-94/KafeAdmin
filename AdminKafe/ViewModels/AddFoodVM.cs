using AdminKafe.Date;
using AdminKafe.Models;
using AdminKafe.View.Windows;
using CV19.Infrastructure.Commands;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AdminKafe.ViewModels
{
    public class AddFoodVM : AbstractClass<Food>, ICommandMethod
    {
        public ICommand MenuFoodCommand { get; set; }
        public ICommand FoodDeleteCommand { get; set; }
        public AddFoodVM()
        {
            CreateCommand = new LambdaCommand(CreateMethod, CanCloseApplicationExecat);
            EditCommand = new LambdaCommand(EditMethod, CanCloseApplicationExecat);
            DeleteCommand = new LambdaCommand(DeleteMethod, CanCloseApplicationExecat);
           ImgSourceCommand = new LambdaCommand(ImgSourceMethod, CanCloseApplicationExecat);
            MenuFoodCommand = new LambdaCommand(AddMenuFoodMethod, CanCloseApplicationExecat);
            FoodDeleteCommand = new LambdaCommand(DeleteFoodCategori, CanCloseApplicationExecat);
            SelectedEdit = new LambdaCommand(EditMethodselect, CanCloseApplicationExecat);
            SelectedEditCommand = new LambdaCommand(SelectedEditMethod, CanCloseApplicationExecat);
            //Task.WhenAll(LoadAllDate1()).ContinueWith(t => IsLoading = false);
        }
        #region Reciep

        private string _SelectedIsCook;
        public string SelectedIsCook
        {
            get { return _SelectedIsCook; }
            set => Set(ref _SelectedIsCook, value);
        }

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

        private Food _SelectedFood1;
        public Food SelectedFood1
        {
            get => _SelectedFood1;
            set
            {
                Set(ref _SelectedFood1, value);
                if (SelectedFood1!=null)
                {
                    Name = SelectedFood1.Name;
                }
            }
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
        private List<Food> _AllFoodMenu;
        public List<Food> AllFoodMenu
        {

            get => _AllFoodMenu;
            set => Set(ref _AllFoodMenu, value);
        }

        private string _IsCok;

        public string IsCok
        {
            get { return _IsCok; }
            set => Set( ref _IsCok, value );
        }

        #endregion Reciept

        public  void CreateMethod(object p)
        {
            IsLoading = true;
              
            Image img = p as Image;
            result = "Запольните поля ";

            if ((Name == null || Name.Replace(" ", "").Length == 0) && SelectedFood == null && Price == 0)
            {
                result += "Названи блюда, ";

            }
            else
            {

                result = DateWorker.CreateFood(Name, Price, SelectedFood, ImgSourse);
                img.Source = null;
                Name = string.Empty;
                ImgSourse = null;
                LoadAllDate();
            }
            OpenOkMethod(result + "!");
            
        }
        public void AddMenuFoodMethod(object p)
        {   
            result = "Запольните поля ";

            if ((Name == null || Name.Replace(" ", "").Length == 0))
            {
                result += "Названи меню  ";
                OpenOkMethod(result + "!");
            }
            else
            {
                result = DateWorker.CreateMenuFood(Name, ImgSourse);
                Name = string.Empty;
                ImgSourse = null;
               LoadAllDate();
            }


        }
        public void DeleteMethod(object p)
        {
            PropertyInfo property = SelectedDateObject.GetType().GetProperty("Id");
            int Id = (int)(property.GetValue(SelectedDateObject, null));
            result = "";
            MessageWindow mv = new MessageWindow("Вы уеронно хотите удалить?");
            mv._mess += x =>
            {
                if (x == 1)
                {
                    result = DateWorker.DeleteFood(Id);
                    LoadAllDate();
                }
            };
            mv.ShowDialog();
        }

        public void EditMethod(object p)
        {
            result = DateWorker.EditCategoriName(SelectedFood1,Name);
            MessageWindowOk f = new MessageWindowOk(result);
            f.ShowDialog();
            Name = "";
            LoadAllDate();
        }
        public void SelectedEditMethod(object p)
        {
            PropertyInfo property = SelectedDateObject.GetType().GetProperty("Id");
            int Id = (int)(property.GetValue(SelectedDateObject, null));
            SelectedEditM(Id);
        }
        int Id = 0;
        public async void SelectedEditM(int tableId)
        {
            await Task.Run(() => {
                try
                {
                    Id = 0;
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var tab = db.Foods.Where(t => t.Id == tableId).Select(t => new {
                            Id = t.Id,
                            Name = t.Name,
                            Price = t.Price,
                            Image = t.Image,
                            ParentCotegory = db.Foods.Where(f => f.Id == t.ParentCategoryId).Select(f => f.Name).FirstOrDefault(),
                            IsCook = t.isCook
                        }).ToList();
                        Id = tab.Select(t => t.Id).FirstOrDefault();
                        Name = tab.Select(t => t.Name).FirstOrDefault();
                        Price = tab.Select(t => t.Price).FirstOrDefault();
                        ImgSourse = tab.Select(t => t.Image).FirstOrDefault();
                        SelectedText = tab.Select(t => t.ParentCotegory).FirstOrDefault();
                        if (tab.Select(t => t.IsCook).FirstOrDefault() == 0)
                            IsCok = "Делается";
                        else if(tab.Select(t => t.IsCook).FirstOrDefault() == 2)
                            IsCok = "Грамм";
                        else
                            IsCok = "Не делается";
                    }

                }
                catch
                {

                }

            });

        }

        public void EditMethodselect(object p)
        {
            if (SelectedIsCook == "Делается")
                result = DateWorker.Edit(Id, Name, Price, SelectedFood, 0, ImgSourse);
            else if (SelectedIsCook == "Грамм")
                result = DateWorker.Edit(Id, Name, Price, SelectedFood, 2, ImgSourse);
            else
                result = DateWorker.Edit(Id, Name, Price, SelectedFood, 1, ImgSourse);
            Name = string.Empty;
            Price = 0.0;
            ImgSourse = null;

            LoadAllDate();
        }

        public override async void LoadAllDate(string name = "")
        {
            IsLoading = true;
            await Task.Run(() =>
            {
                AllDate = DateWorker.GetAllFood();
                AllFoodMenu = DateWorker.GetAllFoodMenu();
            }).ContinueWith(t => IsLoading = false); 
        }   
        
        private void DeleteFoodCategori(object o)
        {
            MessageWindow mv = new MessageWindow("Вы уеронно хотите удалить?");
            mv._mess += x =>
            {
                if (x == 1)
                {
                    result = DateWorker.DeleteCategoriName(SelectedFood1);
                    Name = "";
                    LoadAllDate();
                }
            };
            mv.ShowDialog();
        }
    }
}

