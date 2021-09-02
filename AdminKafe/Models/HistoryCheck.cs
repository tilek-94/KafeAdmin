using System;

namespace AdminKafe.Models
{
    public class HistoryCheck
    {
        public int Id { get; set; }
        public string WaiterName { get; set; }
        public double Salary { get; set; }
        public string SalaryType { get; set; }
        public int GuestCount { get; set; }
        public int Status { get; set; }
        public DateTime CheckDate { get; set; }
    }
}
