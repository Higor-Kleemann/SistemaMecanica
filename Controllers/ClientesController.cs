using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class ClientesController : Controller
{
    private readonly AppDbContext _db;

    public ClientesController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(string? busca)
    {
        var query = _db.Clientes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            var termo = busca.ToLower();
            query = query.Where(c =>
                (c.NomeCompleto != null && c.NomeCompleto.ToLower().Contains(termo)) ||
                (c.CPF != null && c.CPF.ToLower().Contains(termo)) ||
                (c.CNPJ != null && c.CNPJ.ToLower().Contains(termo)));
        }

        ViewBag.Busca = busca;

        var clientes = await query.ToListAsync();
        return View(clientes);
    }

//-------------------------------------------------------------------------------
 
    [HttpGet] 
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        if(ModelState.IsValid)
        {
            _db.Clientes.Add(cliente);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(cliente);
    }

//-------------------------------------------------------------------------------
 
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var cliente = await _db.Clientes.FindAsync(id);
        if (cliente == null) return NotFound();
        return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cliente cliente)
    {
        if(id != cliente.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _db.Clientes.Update(cliente);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(cliente);
    }

//-------------------------------------------------------------------------------
 
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var cliente = await _db.Clientes.Include(c => c.Veiculos).FirstOrDefaultAsync(c => c.Id == id);
        if (cliente == null) return NotFound();
        return View(cliente);
    }

//-------------------------------------------------------------------------------
 
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _db.Clientes.FindAsync(id);
        if (cliente == null) return NotFound();
        return View(cliente);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cliente = await _db.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _db.Clientes.Remove(cliente);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    
//-------------------------------------------------------------------------------
 
    
}