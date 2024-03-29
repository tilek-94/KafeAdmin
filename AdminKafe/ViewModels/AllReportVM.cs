﻿using AdminKafe.Models;
using AdminKafe.View.Windows;
using CV19.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AdminKafe.ViewModels
{
    public class AllReportVM : AbstractClass<Check>, ICommandMethod
    {
        public AllReportVM()
        {
            CreateCommand = new LambdaCommand(CreateMethod, CanCloseApplicationExecat);
            ShowResultCommand = new LambdaCommand(ShowResult, CanShowResult);
            ViewDohodCommand = new LambdaCommand(DohodShow, CanShowResult);
            DeleteCommand = new LambdaCommand(DeleteMethod, CanCloseApplicationExecat);
            ShowWindowCommand = new LambdaCommand(ShowWindowMethod, CanCloseApplicationExecat);
            //AllObjectDate = DateWorker.GetAll();
            LoadWastedFood();
            LoadAllWaters();
            LoadAllBy();
            LoadDohodList();
        }

        private void DohodShow(object o) 
        {
            DohodList = new ObservableCollection<DohodClass>();
            DohodList = DateWorker.GetAllDohod(SelectedFirstDate,SelectedSecondDate);
            ReturnPricces(DateWorker.GetPP(SelectedFirstDate, SelectedSecondDate));

        }
        private async void LoadDohodList()
        {
            await Task.Run(() =>
            {
                DohodList = new ObservableCollection<DohodClass>();
                DohodList = DateWorker.GetAllDohod(DateTime.Now.Date, DateTime.Now);
                // AllLocation = DateWorker.GetAllLocation();
                ReturnPricces(DateWorker.GetPP(DateTime.Now.Date, DateTime.Now));
            });
        }

        private void ReturnPricces((double pt,double pp,double ar,double cp) p)
        {
            PriblText = p.pt;
            ProductPrice = p.pp;
            AllRashod = p.ar;
            ChistPrib = p.cp;
        }

        private DateTime _SelectedOneDay = DateTime.Now.Date;
        public DateTime SelectedOneDay
        {
            get { return _SelectedOneDay; }
            set 
            { 
                Set(ref _SelectedOneDay, value);
                SelectedFirstDate = value;
                SelectedSecondDate = value;
            }
        }
        private DateTime _SelectedFirstDate = DateTime.Now.Date;
        public DateTime SelectedFirstDate
        {
            get { return _SelectedFirstDate; }
            set { Set(ref _SelectedFirstDate, value); }
        }
        private DateTime _SelectedSecondDate = DateTime.Now.Date;
        public DateTime SelectedSecondDate
        {
            get { return _SelectedSecondDate; }
            set { Set(ref _SelectedSecondDate, value); }
        }

        private double _PriblText=0;
        public double PriblText
        {
            get { return _PriblText; }
            set { Set(ref _PriblText, value); }
        }
        private double _ProductPrice=0;
        public double ProductPrice
        {
            get { return _ProductPrice; }
            set { Set(ref _ProductPrice, value); }
        }
        private double _AllRashod=0;
        public double AllRashod
        {
            get { return _AllRashod; }
            set { Set(ref _AllRashod, value); }
        }
        private double _ChistPrib=0;
        public double ChistPrib
        {
            get { return _ChistPrib; }
            set { Set(ref _ChistPrib, value); }
        }

        #region WaiterProperties

        private ObservableCollection<DohodClass> _DohodList;
        public ObservableCollection<DohodClass> DohodList
        {
            get { return _DohodList; }
            set { Set(ref _DohodList, value); }
        }

        private DateTime _FirstDate = DateTime.Now.Date;
        public DateTime FirstDate
        {
            get { return _FirstDate; }
            set { Set(ref _FirstDate, value); }
        }

        private DateTime _SecondDate = DateTime.Now.Date;
        public DateTime SecondDate
        {
            get { return _SecondDate; }
            set { Set(ref _SecondDate, value); }
        }

        private List<Location> allLocation;
        public List<Location> AllLocation
        {
            get => allLocation;
            set => Set(ref allLocation, value);
        }
        private CheckFoodName _SelectedProperties;
        public CheckFoodName SelectedProperties
        {
            get => _SelectedProperties;
            set
            {
                Set(ref _SelectedProperties, value);
                if (value != null)
                {
                    ShowOreders(value.Id, value.GuestCount);
                }


            }
        }

        private int _NumberCheck;
        public int NumberCheck
        {
            get => _NumberCheck;
            set
            {
                Set(ref _NumberCheck, value);
            }
        }
        private string _WaiterProperties;
        public string WaiterProperties
        {
            get => _WaiterProperties;
            set
            {
                Set(ref _WaiterProperties, value);
                LoadAllDateWithDate();
            }
        }
        private string _FilterText = "";
        public string FilterText
        {
            get { return _FilterText; }
            set
            {
                Set(ref _FilterText, value);
                OtdelFilterText(value);
            }

        }
        private void OtdelFilterText(string filter)
        {
            var result = DateWorker.GetAllWastedFood(FirstDate, SecondDate, "");
            OrdersPropertyReport = new ObservableCollection<CoolGet>(result.Where(i => i.FoodName.Contains(filter)));
        }

        private DateTime _DateTimeProperties;
        public DateTime DateTimeProperties
        {
            get => _DateTimeProperties;
            set
            {
                Set(ref _DateTimeProperties, value);
            }
        }

        private DateTime _DateTimeForOneDay = DateTime.Now;
        public DateTime DateTimeForOneDay
        {
            get => _DateTimeForOneDay;
            set
            {
                Set(ref _DateTimeForOneDay, value);
                LoadAllDateWithDate();

            }
        }

        private DateTime _DateTimeForStartDay = DateTime.Now.Date;
        public DateTime DateTimeForStartDay
        {
            get => _DateTimeForStartDay;
            set
            {
                Set(ref _DateTimeForStartDay, value);
                LoadAllDateWithDate();

            }
        }

        private DateTime _DateTimeForEndDay = DateTime.Now.Date;
        public DateTime DateTimeForEndDay
        {
            get => _DateTimeForEndDay;
            set
            {
                Set(ref _DateTimeForEndDay, value);
                LoadAllDateWithDate();

            }
        }

        private string _TableProperties;
        public string TableProperties
        {
            get => _TableProperties;
            set
            {
                Set(ref _TableProperties, value);
            }
        }

        private double _SumCheck;
        public double SumCheck
        {
            get => _SumCheck;
            set
            {
                Set(ref _SumCheck, value);
            }
        }
        private double _SummService;
        public double SummService
        {
            get => _SummService;
            set
            {
                Set(ref _SummService, value);
            }
        }

        private string _StatusProperties;
        public string StatusProperties
        {
            get => _StatusProperties;
            set
            {
                Set(ref _StatusProperties, value);
            }
        }

        private List<CheckFoodClass> _OrdersProperty;
        public List<CheckFoodClass> OrdersProperty
        {
            get => _OrdersProperty;
            set
            {
                Set(ref _OrdersProperty, value);

            }
        }

        private ObservableCollection<CoolGet> _OrdersPropertyReport;
        public ObservableCollection<CoolGet> OrdersPropertyReport
        {
            get => _OrdersPropertyReport;
            set
            {
                Set(ref _OrdersPropertyReport, value);
            }
        }
        #endregion WaiterProperties

        #region ShowResultRegion
        public void ShowResult(object p)
        {
            LoadWastedFood(FirstDate, SecondDate, "");
        }
        public bool CanShowResult(object p)
        {
            return true;
        }
        #endregion

        public void CreateMethod(object p)
        {

        }
        public void DeleteMethod(object p)
        {
            PropertyInfo property = SelectedDateObject.GetType().GetProperty("Id");
            int Id = (int)(property.GetValue(SelectedDateObject, null));
            MessageWindow mv = new MessageWindow("Вы уверены, что хотите удалить?");
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
            throw new NotImplementedException();
        }
        public async void LoadAllBy(string name = "")
        {
            IsLoading = true;
            await Task.Run(() =>
            {
                // AllLocation = DateWorker.GetAllLocation();
            }).ContinueWith(t => IsLoading = false);

        }
        public override async void LoadAllDate(string name = "")
        {
            IsLoading = true;
            await Task.Run(() =>
            {
                //AllObjectDate = DateWorker.GetAll();
                // AllLocation = DateWorker.GetAllLocation();
            }).ContinueWith(t => IsLoading = false);

        }
        public void ShowWindowMethod(object p)
        {
            AddMenuFoodWindow mf = new AddMenuFoodWindow();
            mf.ShowDialog();
        }
        private async void ShowOreders(int Id, int goustCount)
        {
            await Task.Run(() =>
            {
                OrdersProperty = new List<CheckFoodClass>();
                OrdersProperty = DateWorker.GetAllOrder(Id, goustCount);
                SumCheck = Math.Round(DateWorker.SummByProduct, 2);
                SummService = Math.Round(DateWorker.SummServices, 2);
            });
        }
        public async void LoadAllDateWithDate()
        {
            await Task.Run(() =>
            {
                AllObjectDate2 = new List<CheckFoodName>();
                AllObjectDate2 = DateWorker.GetAllCkeck(DateTimeForOneDay, DateTimeForStartDay, DateTimeForEndDay, WaiterProperties);
            });

        }
        public async void LoadAllWaters()
        {
            await Task.Run(() =>
            {
                Waters = DateWorker.GetAllWaiters();
                // AllLocation = DateWorker.GetAllLocation();
            });
        }
        public async void LoadWastedFood(DateTime first, DateTime second, string searchtext = "")
        {
            await Task.Run(() =>
            {
                _OrdersPropertyReport = new ObservableCollection<CoolGet>();
                OrdersPropertyReport = DateWorker.GetAllWastedFood(first, second, searchtext);
                // AllLocation = DateWorker.GetAllLocation();
            });
        }
        public async void LoadWastedFood()
        {
            await Task.Run(() =>
            {
                _OrdersPropertyReport = new ObservableCollection<CoolGet>();
                OrdersPropertyReport = DateWorker.GetAllWastedFood(DateTime.Now.AddDays(-1), DateTime.Now, "");
                // AllLocation = DateWorker.GetAllLocation();
            });
        }
    }
}

