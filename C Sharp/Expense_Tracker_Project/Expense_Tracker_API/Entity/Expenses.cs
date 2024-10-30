namespace Expense_Tracker_API.Entity
{
    public class Expenses
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public decimal? AmountSpend { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string CategoryName { get; set; }
        
        public Decimal BudgetAmount { get; set; }

        public int Category_Count{ get; set; }

        public string? Month_Name{ get; set; }




    }
}
