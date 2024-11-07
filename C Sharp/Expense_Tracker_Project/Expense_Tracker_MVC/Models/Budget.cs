using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker_MVC.Models
{
    public class Budget : IValidatableObject
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public decimal BudgetAmount { get; set; }

        public decimal Balance { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TimeFrame { get; set; }

        public string CategoryName { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult("EndDate must be greater than StartDate");
            }
        }
    }
}


