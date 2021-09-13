using AdminKafe.Models;
using AdminKafe.View.Windows;
using CV19.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminKafe.ViewModels
{
    public class AddServiceVM : AbstractClass<Consumption>, ICommandMethod
    {
        public AddServiceVM()
        {
            CreateCommand = new LambdaCommand(CreateMethod, CanCloseApplicationExecat);
            DeleteCommand = new LambdaCommand(DeleteMethod, CanCloseApplicationExecat);
            EditCommand = new LambdaCommand(EditMethod, CanCloseApplicationExecat);
        }
        #region WaiterProperties


        private double _Summ;
        public double Summ
        {
            get => _Summ;
            set => Set(ref _Summ, value);
        }



        #endregion WaiterProperties

        public void CreateMethod(object p)
        {
            result = "Заполните поля ";
            if (Name == "")
            {
                result += "Название блюда, ";

            }
            else
            {
                result = DateWorker.CreateConsumption(Name, Summ);
                ClearMetod();
                LoadAllDate();
            }
            OpenOkMethod(result + "!");
        }

        public void DeleteMethod(object p)
        {
            result = "";
            MessageWindow mv = new MessageWindow("Вы уверены,что хотите удалить?");
            mv._mess += x =>
            {
                if (x == 1)
                {
                   result = DateWorker.DeleteConsumption(SelectedDate);
                    LoadAllDate();
                    ClearMetod();
                }
            };
            mv.ShowDialog();
        }

        public void EditMethod(object p)
        {
            if (SelectedDate!=null && Name !=String.Empty)
            {
                result = DateWorker.EditConsumption(SelectedDate.Id, Name, Summ);
                MessageWindowOk wm = new MessageWindowOk(result);
                wm.ShowDialog();
                LoadAllDate();
                ClearMetod();
            }
            else
            {
                MessageWindowOk wm = new MessageWindowOk("Выберите услуги");
                wm.ShowDialog();
            }
        }
        private void ClearMetod()
        {
            SelectedDate = null;
            Name = "";
            Summ = 0;
        }
        public override async void LoadAllDate(string name = "")
        {
            IsLoading = true;
            await Task.Run(() =>
            {
                AllDate = DateWorker.GetAllConsumption(name);
            }).ContinueWith(t => IsLoading = false);

        }


    }
}

