namespace Expense_Tracker_MVC.Models
{
    public class CommonModel
    {
        public IEnumerable<Expenses> Expenses { get; set; }
        public IEnumerable<CategorySpend> CategorySpend { get; set; }
        
    }
}
