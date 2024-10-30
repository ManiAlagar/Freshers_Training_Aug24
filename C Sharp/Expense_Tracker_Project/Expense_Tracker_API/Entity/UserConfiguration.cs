namespace Expense_Tracker_API.Entity
{
    public class UserConfiguration
    {
        public int Id { get; set; }

        public int UserID { get; set; }
        public string ConfigKey { get; set; }

        public string ConfigValue { get; set; }
    }
}
