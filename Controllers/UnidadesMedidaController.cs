using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class UnidadesMedidaController : Controller
{
    private readonly AppDbContext _db;

    public UnidadesMedidaController(AppDbContext db)
    {
        _db = db;
    }

//-------------------------------------------------------------------------------

    public async Task<IActionResult> Index()
    {
        var unidadeMedida = await _db.UnidadesMedida.ToListAsync();
        return View(unidadeMedida);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UnidadeMedida unidadeMedida)
    {
        if (ModelState.IsValid)
        {
            _db.UnidadesMedida.Add(unidadeMedida);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(unidadeMedida);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var unidadeMedida = await _db.UnidadesMedida.FindAsync(id);
        if (unidadeMedida == null) return NotFound();
        return View(unidadeMedida);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UnidadeMedida unidadeMedida)
    {
        if (id != unidadeMedida.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _db.UnidadesMedida.Update(unidadeMedida);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(unidadeMedida);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var unidadeMedida = await _db.UnidadesMedida.FindAsync(id);
        if (unidadeMedida == null) return NotFound();
        return View(unidadeMedida);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var unidadeMedida = await _db.UnidadesMedida.FindAsync(id);
        if (unidadeMedida != null)
        {
            _db.UnidadesMedida.Remove(unidadeMedida);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}