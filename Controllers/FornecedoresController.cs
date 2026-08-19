using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class FornecedoresController : Controller
{
    private readonly AppDbContext _db;

    public FornecedoresController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(string? busca)
    {
        var query = _db.Fornecedores.AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            var termo = busca.ToLower();
            query = query.Where(f => f.CNPJ != null && f.CNPJ.ToLower().Contains(termo));
        }

        ViewBag.Busca = busca;

        var fornecedores = await query.ToListAsync();
        return View(fornecedores);
    }

//-------------------------------------------------------------------------------
 
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Fornecedor fornecedor)
    {
        if (ModelState.IsValid)
        {
            _db.Fornecedores.Add(fornecedor);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(fornecedor);
    }

//-------------------------------------------------------------------------------
 
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var fornecedor = await _db.Fornecedores.FindAsync(id);
        if (fornecedor == null) return NotFound();
        return View(fornecedor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Fornecedor fornecedor)
    {
        if (id != fornecedor.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _db.Fornecedores.Update(fornecedor);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(fornecedor);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var fornecedor = await _db.Fornecedores.FindAsync(id);
        if (fornecedor == null) return NotFound();
        return View(fornecedor);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var fornecedor = await _db.Fornecedores.FindAsync(id);
        if (fornecedor == null) return NotFound();
        return View(fornecedor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var fornecedor = await _db.Fornecedores.FindAsync(id);
        if (fornecedor != null)
        {
            _db.Fornecedores.Remove(fornecedor);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}