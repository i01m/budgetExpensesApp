using System.Linq;

namespace BudgetMySpending.Models
{
    public interface IExpenseRepository
    { 
        IQueryable<Expense> Expenses { get; }
        void SaveExpense(Expense expense);

        decimal ExpensesTotal();

        Expense DeleteExpense(int expenseID);

        bool DeleteAllExpenses();
    }
}
