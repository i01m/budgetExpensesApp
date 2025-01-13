using BudgetMySpending.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace BudgetMySpending.Controllers
{
    public class HomeController : Controller
    {
        public IExpenseRepository expenseRepository;
        public IBudgetRepository budgetRepository;

        public HomeController(IExpenseRepository repo, IBudgetRepository repoBudget)
        {
            expenseRepository = repo;
            budgetRepository = repoBudget;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Details");
            }


            return View();
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Details()
        {          
            var countBudgets = budgetRepository.Budgets.Count();
            if (countBudgets == 0)
            {                
                var deleteExpenses = expenseRepository.DeleteAllExpenses();
                if (deleteExpenses)
                {
                    TempData["deletedExpensesAlert"] = "Since the Budget is $0, all Expenses were deleted";
                }                
                TempData["BudgetToDisplay"] = 0;
                TempData["ExpensesTotalToDisplay"] = (int)expenseRepository.ExpensesTotal();
                TempData["RemainingBalance"] = 0;
            }
            else
            {
                return RedirectToAction(nameof(CalculateComplete));

            }

            return View(expenseRepository.Expenses);
        }

        [HttpPost]
        public IActionResult Calculate(Budget budget)
        {
            if (budget.Amount != 0)
            {
                budgetRepository.SaveBudget(budget);
            }

            return RedirectToAction(nameof(CalculateComplete));
        }
        
        public ViewResult CalculateComplete()
        {       
            var budgetToDisplay = (int)budgetRepository.latestBudgetAmount();
            
            TempData["BudgetToDisplay"] = budgetToDisplay;          
            TempData["ExpensesTotalToDisplay"] = (int)expenseRepository.ExpensesTotal(); 
            TempData["RemainingBalance"] = (int)budgetToDisplay - (int)expenseRepository.ExpensesTotal();        

            return View("Details", expenseRepository.Expenses);
        }

        [HttpPost]
        public IActionResult AddExpense(Expense expense)
        {
            var budgetsCount = budgetRepository.Budgets.Count();
            if (budgetsCount == 0)
            {
                TempData["NeedToEnterBudgetFirst"] = "Before procedding please enter Budget Amount";

                return RedirectToAction("Details");
            }
            
            if (expense.Amount != 0)
            {
                expenseRepository.SaveExpense(expense);
            }
                       
            return RedirectToAction(nameof(CalculateComplete));
        }
               
  
        [HttpPost]
        public IActionResult Delete(int expenseId)
        {
            Expense deletedExpense = expenseRepository.DeleteExpense(expenseId);

            if (deletedExpense != null)
            {
                TempData["message"] = $"{deletedExpense.Name} was deleted";
            }
            return RedirectToAction(nameof(CalculateComplete));
        }


        public ViewResult Edit(int expenseId) =>
            View(expenseRepository.Expenses.FirstOrDefault(e => e.ExpenseID == expenseId));

        [HttpPost]
        public IActionResult Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                expenseRepository.SaveExpense(expense);
                TempData["message"] = $"{expense.Name} has been saved";
                return RedirectToAction(nameof(CalculateComplete));
            }
            else
            {
                // there is something wrong with the data values
                return View(expense);
            }
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
