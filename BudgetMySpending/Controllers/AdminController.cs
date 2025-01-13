using BudgetMySpending.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetMySpending.Controllers
{
    public class AdminController : Controller
    {
        private IBudgetRepository budgetsRepository;

        public AdminController(IBudgetRepository budgetsRepo)
        {
            budgetsRepository = budgetsRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteAllBudgets()
        {
            var result = budgetsRepository.DeleteAllBudgets();

            if (result == true)
            {
                TempData["adminMessage"] = $"All records were deleted from the Budgets Table ";
            }
            else
            {
                TempData["adminMessage"] = $"There are no records to delete in Budget Table";
            }

            return RedirectToAction("DeleteAllBudgetsComplete");
        }

        public ViewResult DeleteAllBudgetsComplete()
        {
            return View("Index");
        }
    }
}