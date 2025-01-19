using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpendSmartDbContext _context;

        public HomeController(ILogger<HomeController> logger, SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            //list out all of our expenses
            var allExpenses = _context.Expenses.ToList();

            //Getting the sum of all our values in the expense table
            var totalExpenses = allExpenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;


            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        { 
            if (id != null) {
                //editing -> Load an expense byId
                var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
                 
                return View(expenseInDb);
            }

            return View();
        }



        //we want to receive the expense as a parameter
        public IActionResult CreateEditExpenseForm(Expense model)
        {
           
            //once the form is saved, we want to redirect it to the expenses page
            if (model.Id == 0)
            {
                //Create
                _context.Expenses.Add(model);
            }
            else
            {
                //Editing
                _context.Expenses.Update(model);
            }
           
            _context.SaveChanges();
             
            return RedirectToAction("Expenses");
        }

        public IActionResult DeleteExpense(int id)
        {
            //This means that we go into our expenses list and we take the first one where the Id matches the Id in the parameter 
            var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();


            return RedirectToAction("Expenses");
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
