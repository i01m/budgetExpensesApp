namespace BudgetMySpending.Models.ViewModels
{
    public class HomeCalculateViewModel
    {
        public string Budget { get; set; }

        public string ExpensesTotal { get; set; }

        public string Balance { get; set; }

        public int ExpenseID { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
