using System;

namespace AdminKafe.Models
{
    public class CheckFoodName
    {
        public int Id { get; set; }
        public int CheckCount { get; set; }
        public DateTime CheckDate { get; set; }
        public string TableName { get; set; }
        public string TableCategoryName { get; set; }
        public string WaiterName { get; set; }
        public int GuestCount { get; set; }
        public string Status { get; set; }
        public int StatusID { get; set; }
        public double CheckSumm { get; set; }
    }
}