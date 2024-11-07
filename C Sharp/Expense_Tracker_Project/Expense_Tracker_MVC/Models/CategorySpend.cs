namespace Expense_Tracker_MVC.Models
{
    public class CategorySpend
    {
        public int UserID { get; set; }

        public string CategoryName { get; set; }

        public decimal AmountSpend { get; set; }

        public decimal Balance { get; set; }

    }
}
