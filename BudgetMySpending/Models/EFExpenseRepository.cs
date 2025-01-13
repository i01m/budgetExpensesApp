using BudgetMySpending.Data;
using System.Collections.Generic;
using System.Linq;

namespace BudgetMySpending.Models
{
    public class EFExpenseRepository : IExpenseRepository
    {
        private ApplicationDbContext context;

        public EFExpenseRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Expense> Expenses => context.Expenses;

        public Expense DeleteExpense(int expenseID)
        {
            Expense dbentry = context.Expenses.FirstOrDefault(e => e.ExpenseID == expenseID);
            if (dbentry != null)
            {
                context.Expenses.Remove(dbentry);
                context.SaveChanges();
            }
            return dbentry;
        }

        public void SaveExpense(Expense expense)
        {
            if (expense.ExpenseID == 0)
            {
                context.Expenses.Add(expense);
            }
            else
            {
                Expense dbentry = context.Expenses.FirstOrDefault(e => e.ExpenseID == expense.ExpenseID);
                if (dbentry.ExpenseID != 0)
                {
                    dbentry.Name = expense.Name;
                    dbentry.Amount = expense.Amount;
                }
            }
            context.SaveChanges();
        }

        decimal IExpenseRepository.ExpensesTotal()
        {
            return context.Expenses.Sum(e => e.Amount);
        }

        public bool DeleteAllExpenses()
        {
            {
                List<Expense> expensesList = context.Expenses.OrderBy(e => e.ExpenseID).ToList();

                foreach (var dbrecord in expensesList)
                {
                    if (dbrecord != null)
                    {
                        context.Expenses.Remove(dbrecord);
                        context.SaveChanges();
                    }
                }

                var count = context.Expenses.Count();
                if (count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
