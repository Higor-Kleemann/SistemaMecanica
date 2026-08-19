using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class ServicosController : Controller
{
    private readonly AppDbContext _db;

    public ServicosController(AppDbContext db)
    {
        _db = db;
    }

//-------------------------------------------------------------------------------

    public async Task<IActionResult> Index()
    {
        var servicos = await _db.Servicos.ToListAsync();
        return View(servicos);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Servico servico)
    {
        if (ModelState.IsValid)
        {
            _db.Servicos.Add(servico);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(servico);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var servico = await _db.Servicos.FindAsync(id);
        if (servico == null) return NotFound();
        return View(servico);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Servico servico)
    {
        if (id != servico.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _db.Servicos.Update(servico);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(servico);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var servico = await _db.Servicos.FindAsync(id);
        if (servico == null) return NotFound();
        return View(servico);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var servico = await _db.Servicos.FindAsync(id);
        if (servico != null)
        {
            _db.Servicos.Remove(servico);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}