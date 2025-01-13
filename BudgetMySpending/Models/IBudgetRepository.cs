using System.Linq;

namespace BudgetMySpending.Models
{
    public interface IBudgetRepository
    {
        IQueryable<Budget> Budgets { get; }
        void SaveBudget(Budget budget);
        decimal latestBudgetAmount();
        bool DeleteAllBudgets();
    }
}
