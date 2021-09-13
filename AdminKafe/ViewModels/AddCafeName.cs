using AdminKafe.Date;
using AdminKafe.Models;
using CV19.Infrastructure.Commands;
using KafeProject.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdminKafe.ViewModels
{
    public class AddCafeName:AbstractClass<AddCafeName>
    {
        public ICommand UpDateCommand { get; set; }
        public AddCafeName()
        {
            UpDateCommand = new LambdaCommand(UpdateMetod, CanCloseApplicationExecat);
            LoadAllDate();
        }
        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name,value);
        }
        private string adress;
        public string Adress
        {
            get => adress;
            set => Set(ref adress, value);
        }
        private string fon;
        public string Fon
        {
            get => fon;
            set => Set(ref fon, value);
        }
        private string printer2;
        public string Printer2
        {
            get => printer2;
            set => Set(ref printer2, value);
        }
        private string printer1;
        public string Printer1
        {
            get => printer1;
            set => Set(ref printer1, value);
        }
        public override void LoadAllDate(string name = "")
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.CafeName.OrderBy(i => i).LastOrDefault();
                if (result != null)
                {
                    Name = result.Name.ToString();
                    Adress = result.Adress.ToString();
                }
                var result1 = db.Options.Where(u=>u.Key== "FonValue");
                foreach (var item in result1)
                {
                    Fon = item.Value;
                }
                Options result2 = (Options)db.Options.FirstOrDefault(u => u.Key == "Printer-1");
                if (result2 != null)
                {
                    printer1 = result2.Value;
                }
                var result3 = db.Options.FirstOrDefault(u => u.Key == "Printer-2");
                if (result3 != null)
                {
                    printer2 = result3.Value;
                }
            }

        }
        public void UpdateMetod(object p)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                CafeName сafeName = db.CafeName.OrderBy(i => i).LastOrDefault();
                сafeName.Name = name;
                сafeName.Adress = adress;
                db.SaveChanges();

                Options options = db.Options.FirstOrDefault(d => d.Key == "FonValue");
                options.Value = Fon;
                db.SaveChanges();

                Options options1 = db.Options.FirstOrDefault(d => d.Key == "Printer-1");
                options1.Value = Printer1;
                db.SaveChanges();

                Options options2 = db.Options.FirstOrDefault(d => d.Key == "Printer-2");
                options2.Value = Printer2;
                db.SaveChanges();
                OpenOkMethod("Успешно сохранено");
            }
        }
    }
}
