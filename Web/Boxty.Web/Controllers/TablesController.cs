namespace Boxty.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Boxty.Data;
    using Boxty.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = "manager,admin")]
    public class TablesController : Controller
    {
        private readonly ITableService tableService;

        public TablesController(ITableService tableService)
        {
            this.tableService = tableService;
        }

        public IActionResult Index()
        {
            return View(tableService.GetTables<TableViewModel>());
        }

        public IActionResult Details(int id)
        {
            var table = tableService.GetTableById(id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Seats")] Table table)
        {
            if (ModelState.IsValid)
            {
                await tableService.CreateTable(table);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var table = tableService.GetTableById(id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            this.tableService.DeleteTable(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TableExists(int id)
        {
            return tableService.GetAllTables().Any(e => e.Id == id);
        }
    }
}
