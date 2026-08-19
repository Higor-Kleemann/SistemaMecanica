using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class EstoqueController : Controller
{
    private readonly AppDbContext _db;
    public EstoqueController(AppDbContext db)
    {
        _db = db;
    }

//-----------------------------------------------------

    public async Task<IActionResult> Index(string? busca)
    {
        var query = _db.Estoques.Include(e => e.Produto).AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            var termo = busca.ToLower();
            query = query.Where(e => e.Produto != null && e.Produto.Nome != null && e.Produto.Nome.ToLower().Contains(termo));
        }

        ViewBag.Busca = busca;

        var estoques = await query.ToListAsync();
        return View(estoques);
    }

//-----------------------------------------------------
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Produtos = await _db.Produtos.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Estoque estoque)
    {
        if (ModelState.IsValid)
        {
            _db.Estoques.Add(estoque);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Produtos = await _db.Produtos.ToListAsync();
        return View(estoque);
    }

//-----------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var estoque = await _db.Estoques.FindAsync(id);
        if(estoque == null) return NotFound();
        ViewBag.Produtos = await _db.Produtos.ToListAsync();
        return View(estoque);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Estoque estoque)
    {
        if(id != estoque.Id) return NotFound();

        if(ModelState.IsValid)
        {
            _db.Estoques.Update(estoque);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Produtos = await _db.Produtos.ToListAsync();
        return View(estoque);
    }

//-----------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Details (int id)
    {
        var estoque = await _db.Estoques.Include(e => e.Produto).FirstOrDefaultAsync(e => e.Id == id);
        if(estoque == null) return NotFound();
        return View(estoque);
    }

//------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var estoque = await _db.Estoques.Include(e => e.Produto).FirstOrDefaultAsync(e => e.Id == id);
        if(estoque == null) return NotFound();
        return View(estoque);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var estoque = await _db.Estoques.FindAsync(id);
        if(estoque != null)
        {
            _db.Estoques.Remove(estoque);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}