using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using To_do_app_donet.Data;
using To_do_app_donet.Models;

namespace To_do_app_donet.Controllers
{
    public class TodosController : Controller
    {
        private readonly To_do_app_donetContext _context;

        public TodosController(To_do_app_donetContext context)
        {
            _context = context;
        }

        // GET: Todos
        public async Task<IActionResult> Index()
        {
              return _context.Todos != null ? 
                          View(await _context.Todos.ToListAsync()) :
                          Problem("Entity set 'To_do_app_donetContext.Todos'  is null.");
        }

        // GET: Todos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todos = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todos == null)
            {
                return NotFound();
            }

            return View(todos);
        }

        // GET: Todos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Todo,IsDone")] Todos todos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todos);
        }

        // GET: Todos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todos = await _context.Todos.FindAsync(id);
            if (todos == null)
            {
                return NotFound();
            }
            return View(todos);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Todo,IsDone")] Todos todos)
        {
            if (id != todos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodosExists(todos.Id))
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
            return View(todos);
        }

        // GET: Todos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todos = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todos == null)
            {
                return NotFound();
            }

            return View(todos);
        }

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Todos == null)
            {
                return Problem("Entity set 'To_do_app_donetContext.Todos'  is null.");
            }
            var todos = await _context.Todos.FindAsync(id);
            if (todos != null)
            {
                _context.Todos.Remove(todos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodosExists(int id)
        {
          return (_context.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
