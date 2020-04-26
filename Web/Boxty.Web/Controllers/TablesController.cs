namespace Boxty.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Boxty.Data;
    using Boxty.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class TablesController : Controller
    {
        private readonly BoxtyDbContext _context;
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            if (ModelState.IsValid)
            {
                await tableService.CreateTable(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Table table)
        {
            if (id != table.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(table.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(table);
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
            return _context.Tables.Any(e => e.Id == id);
        }
    }
}
