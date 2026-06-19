using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class CategoriasController : Controller
{
    private readonly AppDbContext _db;

    public CategoriasController(AppDbContext db)
    {
        _db = db;
    }

//-------------------------------------------------------------------------------

    public async Task<IActionResult> Index()
    {
        var categorias = await _db.Categorias.ToListAsync();
        return View(categorias);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            _db.Categorias.Add(categoria);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categoria);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _db.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();
        return View(categoria);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Categoria categoria)
    {
        if (id != categoria.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _db.Categorias.Update(categoria);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categoria);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _db.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();
        return View(categoria);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categoria = await _db.Categorias.FindAsync(id);
        if (categoria != null)
        {
            _db.Categorias.Remove(categoria);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}