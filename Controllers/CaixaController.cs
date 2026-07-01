using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class CaixaController : Controller
{
    private readonly AppDbContext _db;

    public CaixaController(AppDbContext db)
    {
        _db = db;
    }

//-------------------------------------------------------------------------------

    public async Task<IActionResult> Index()
    {
        var registros = await _db.Caixas
            .Include(c => c.OrdemServico)
            .ToListAsync();
        return View(registros);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.OrdensServico = await _db.OrdensServico
            .Include(o => o.Cliente)
            .ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Caixa caixa)
    {
        if (ModelState.IsValid)
        {
            caixa.DataRegistro = DateTime.Now;
            _db.Caixas.Add(caixa);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.OrdensServico = await _db.OrdensServico
            .Include(o => o.Cliente)
            .ToListAsync();
        return View(caixa);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var caixa = await _db.Caixas.FindAsync(id);
        if (caixa == null) return NotFound();

        ViewBag.OrdensServico = await _db.OrdensServico
            .Include(o => o.Cliente)
            .ToListAsync();
        return View(caixa);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Caixa caixa)
    {
        if (id != caixa.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _db.Caixas.Update(caixa);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.OrdensServico = await _db.OrdensServico
            .Include(o => o.Cliente)
            .ToListAsync();
        return View(caixa);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var caixa = await _db.Caixas
            .Include(c => c.OrdemServico)
                .ThenInclude(o => o.Cliente)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (caixa == null) return NotFound();
        return View(caixa);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var caixa = await _db.Caixas
            .Include(c => c.OrdemServico)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (caixa == null) return NotFound();
        return View(caixa);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var caixa = await _db.Caixas.FindAsync(id);
        if (caixa != null)
        {
            _db.Caixas.Remove(caixa);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}