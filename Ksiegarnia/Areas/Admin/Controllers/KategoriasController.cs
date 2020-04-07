using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ksiegarnia.DataAccess.Data;
using Ksiegarnia.Models;

namespace Ksiegarnia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Kategorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategorie.ToListAsync());
        }

        // GET: Admin/Kategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategoria == null)
            {
                return NotFound();
            }

            return View(kategoria);
        }

        // GET: Admin/Kategorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Kategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa")] Kategoria kategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategoria);
        }

        // GET: Admin/Kategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategorie.FindAsync(id);
            if (kategoria == null)
            {
                return NotFound();
            }
            return View(kategoria);
        }

        // POST: Admin/Kategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa")] Kategoria kategoria)
        {
            if (id != kategoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriaExists(kategoria.Id))
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
            return View(kategoria);
        }

        // GET: Admin/Kategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategoria == null)
            {
                return NotFound();
            }

            return View(kategoria);
        }

        // POST: Admin/Kategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategoria = await _context.Kategorie.FindAsync(id);
            _context.Kategorie.Remove(kategoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriaExists(int id)
        {
            return _context.Kategorie.Any(e => e.Id == id);
        }
    }
}
