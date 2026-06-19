using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class MecanicosController : Controller
{
    private readonly AppDbContext _db;

    public MecanicosController(AppDbContext db)
    {
        _db = db;
    }

//-------------------------------------------------------------------------------
 
    public async Task<IActionResult> Index()
    {
        var mecanicos = await _db.Mecanicos.ToListAsync();
        return View(mecanicos);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Mecanico mecanico)
    {
        if (ModelState.IsValid)
        {
            _db.Mecanicos.Add(mecanico);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(mecanico);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var mecanico = await _db.Mecanicos.FindAsync(id);
        if (mecanico == null) return NotFound();
        return View(mecanico);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Mecanico mecanico)
    {
        if (id != mecanico.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _db.Mecanicos.Update(mecanico);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(mecanico);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var mecanico = await _db.Mecanicos.FindAsync(id);
        if (mecanico == null) return NotFound();
        return View(mecanico);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var mecanico = await _db.Mecanicos.FindAsync(id);
        if (mecanico == null) return NotFound();
        return View(mecanico);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var mecanico = await _db.Mecanicos.FindAsync(id);
        if (mecanico != null)
        {
            _db.Mecanicos.Remove(mecanico);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}