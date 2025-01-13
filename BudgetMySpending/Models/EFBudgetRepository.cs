using BudgetMySpending.Data;
using System.Collections.Generic;
using System.Linq;

namespace BudgetMySpending.Models
{
    public class EFBudgetRepository : IBudgetRepository
    {
        ApplicationDbContext context;
                     
        public EFBudgetRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Budget> Budgets => context.Budgets;

        public bool DeleteAllBudgets()
        {
            List<Budget> budgetsList = context.Budgets.OrderBy(b => b.BudgetID).ToList();

            foreach (var dbrecord in budgetsList)
            {
                if (dbrecord != null)
                {
                    context.Budgets.Remove(dbrecord);
                    context.SaveChanges();
                }
            }

            var count = context.Budgets.Count();
            if (count == 0)
            {
                return true;
            } 
            else
            { 
               return false; 
            }
        }

        public decimal latestBudgetAmount()
        {
            return Budgets.OrderByDescending(b => b.BudgetID).FirstOrDefault().Amount;           
        }

        public void SaveBudget(Budget budget)
        {
            if (budget.BudgetID == 0)
            {
                context.Budgets.Add(budget);
            }
            else
            {
                Budget dbentry = context.Budgets.FirstOrDefault(e => e.BudgetID == budget.BudgetID);
                if (dbentry.BudgetID != 0)
                {
                    dbentry.Amount = budget.Amount;
                }
            }
            context.SaveChanges();
        }
    }
}



