using AdminKafe.Models;
using AdminKafe.View.Windows;
using CV19.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdminKafe.ViewModels
{
    public class AddTableVM : AbstractClass<Table>, ICommandMethod
    {
        public ICommand DeleteTableCategory { get; set; }
        public ICommand EditLocation { get; set; }
        public ICommand ClearCommand { get; set; } 
        public ICommand EditTableCommand { get; set; }
        public AddTableVM()
        {
            EditTableCommand = new LambdaCommand(EditTableMetod, CanCloseApplicationExecat);
            CreateCommand = new LambdaCommand(CreateMethod, CanCloseApplicationExecat);
            DeleteCommand = new LambdaCommand(DeleteMethod, CanCloseApplicationExecat);
            ShowWindowCommand = new LambdaCommand(ShowWindowMethod, CanCloseApplicationExecat);
            CreatMenuCommand = new LambdaCommand(CreateCategoryName, CanCloseApplicationExecat);
            DeleteTableCategory = new LambdaCommand(DeleteTableCategoryMetod, CanCloseApplicationExecat);
            EditCommand = new LambdaCommand(EditMethod, CanCloseApplicationExecat);
            EditLocation = new LambdaCommand(EditLocationMetod, CanCloseApplicationExecat);
            ClearCommand = new LambdaCommand(ClearMetod, CanCloseApplicationExecat);
        }
        #region WaiterProperties

        private string searchTable;
        public string SearchTable
        {
            get => searchTable;
            set
            {
                Set(ref searchTable, value);
                AllObjectDate = DateWorker.GetAllTablesSearch(searchTable);
            }
        }
        private List<Location> allLocation;
        public List<Location> AllLocation
        {
            get => allLocation;
            set => Set(ref allLocation, value);
        }
        private Location _SelectedLocation;
        public Location SelectedLocation
        {
            get => _SelectedLocation;
            set => Set(ref _SelectedLocation, value);
        }

        private string _Tel;
        public string Tel
        {
            get => _Tel;
            set => Set(ref _Tel, value);
        }

        private string _CategoryName=String.Empty;
        public string CategoryName
        {
            get => _CategoryName;
            set => Set(ref _CategoryName, value);
        }
        private string _Adress;
        public string Adress
        {
            get => _Adress;
            set => Set(ref _Adress, value);
        }
        private string _TableName;
        public string TableName
        {
            get => _TableName;
            set => Set(ref _TableName, value);
        }

        #endregion WaiterProperties

        public void CreateMethod(object p)
        {
            result = "Запольните поля ";
            int flag = 0;
            if (TableName == null || TableName.Replace(" ", "").Length == 0)
            {
                result += "Ф.И.О, ";
                flag = 1;
            }
            if (SelectedLocation == null)
            {
                result += "Комната, ";
                flag = 1;
            }

            if (flag == 0)
            {
                result = DateWorker.CreateTable(TableName, SelectedLocation);
                TableName = string.Empty;

            }
            LoadAllDate();
            OpenOkMethod(result);
        }

        public void DeleteMethod(object p)
        {
            PropertyInfo property = SelectedDateObject.GetType().GetProperty("Id");
            int Id = (int)(property.GetValue(SelectedDateObject, null));
            MessageWindow mv = new MessageWindow("Вы уеронно хотите удалить?");
            mv._mess += x =>
            {
                if (x == 1)
                {
                    result = DateWorker.DeleteTable(Id);
                    LoadAllDate();
                }
            };
            mv.ShowDialog();
        }

        public void EditMethod(object p)
        {
            CategoryName = SelectedLocation.Name;
        }

        public override async void LoadAllDate(string name = "")
        {
            IsLoading = true;
            await Task.Run(() =>
            {
                AllObjectDate = DateWorker.GetAllTables();
                AllLocation = DateWorker.GetAllLocation();
            }).ContinueWith(t => IsLoading = false);

        }

        public void ShowWindowMethod(object p)
        {
            AddTableCategory mf = new AddTableCategory();
            mf.ShowDialog();
        }

        private void CreateCategoryName(object o)
        {
            if (CategoryName==String.Empty)
            {
                MessageWindowOk wm = new MessageWindowOk("Запольните поля");
                wm.ShowDialog();
            }
            else
            {
                result = DateWorker.CreateLocation(CategoryName);
                MessageWindowOk wm = new MessageWindowOk(result);
                wm.ShowDialog();
                LoadAllDate();
                CategoryName = "";
            }
        }
        private void DeleteTableCategoryMetod(object o)
        {
            MessageWindow mv = new MessageWindow("Вы уеронно хотите удалить?");
            mv._mess += x =>
            {
                if (x == 1)
                {
                    result = DateWorker.DeleteLocation(SelectedLocation);
                    LoadAllDate();
                    CategoryName = "";
                }
            };
            mv.ShowDialog();
        }

        private void EditLocationMetod(object o)
        {
            if (CategoryName == String.Empty)
            {
                MessageWindowOk wm = new MessageWindowOk("Запольните поля");
                wm.ShowDialog();
            }
            else
            {
                if (SelectedLocation!=null)
                {
                    result = DateWorker.EditLocation(SelectedLocation, CategoryName);
                    MessageWindowOk wm = new MessageWindowOk(result);
                    wm.ShowDialog();
                    LoadAllDate();
                    CategoryName = "";
                }
            }
        }

        private void ClearMetod(object o)
        {
            SelectedLocation = null;
            TableName = "";
        }
        private void EditTableMetod(object o)
        {
            if (TableName != String.Empty)
            {
            //    MessageWindowOk wm = new MessageWindowOk("Запольните поля");
            //    wm.ShowDialog();
            //}
            //else
            //{
                if (SelectedLocation != null)
                {
                    result = DateWorker.EditTabel(DateWorker.AllId, TableName, SelectedLocation);
                    MessageWindowOk wm = new MessageWindowOk(result);
                    wm.ShowDialog();
                    LoadAllDate();
                    SelectedLocation = null;
                    TableName = "";
                }
            }
        }
    }
}
