namespace Expense_Tracker_MVC.Models
{
    public class Expenses
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public decimal AmountSpend { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string CategoryName { get; set; }
    }
}