using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class ProdutosController : Controller
{
    private readonly AppDbContext _db;

    public ProdutosController(AppDbContext db)
    {
        _db = db;
    }

//-------------------------------------------------------------------------------    

    public async Task<IActionResult> Index()
    {
        var produtos = await _db.Produtos.Include(p => p.Categoria).Include(p => p.UnidadeMedida).ToListAsync();
        return View(produtos);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categorias = await _db.Categorias.ToListAsync();
        ViewBag.UnidadesMedida = await _db.UnidadesMedida.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create (Produto produto)
    {
        if (ModelState.IsValid)
        {
            _db.Produtos.Add(produto);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.Categorias = await _db.Categorias.ToListAsync();
        ViewBag.UnidadesMedida = await _db.UnidadesMedida.ToListAsync();
        return View(produto);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var produto = await _db.Produtos.FindAsync(id);
        if(produto == null) return NotFound();

        ViewBag.Categorias = await _db.Categorias.ToListAsync();
        ViewBag.UnidadesMedida = await _db.UnidadesMedida.ToListAsync();
        return View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Produto produto)
    {
        if(id != produto.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _db.Produtos.Update(produto);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Categorias = await _db.Categorias.ToListAsync();
        ViewBag.UnidadesMedida = await _db.UnidadesMedida.ToListAsync();
        return View(produto);
    }

//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var produto = await _db.Produtos.Include(p => p.Categoria).Include(p => p.UnidadeMedida).FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return NotFound();
        return View(produto);
    }

//-------------------------------------------------------------------------------

[HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _db.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.UnidadeMedida)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null) return NotFound();
        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _db.Produtos.FindAsync(id);
        if (produto != null)
        {
            _db.Produtos.Remove(produto);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}