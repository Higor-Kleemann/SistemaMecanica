using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class VeiculosController : Controller
{
    private readonly AppDbContext _db;

    public VeiculosController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(string? busca)
    {
        var query = _db.Veiculos.Include(v => v.Cliente).AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            var termo = busca.ToLower();
            query = query.Where(v =>
                (v.Placa != null && v.Placa.ToLower().Contains(termo)) ||
                (v.Cliente != null && v.Cliente.NomeCompleto != null && v.Cliente.NomeCompleto.ToLower().Contains(termo)));
        }

        ViewBag.Busca = busca;

        var veiculos = await query.ToListAsync();
        return View(veiculos);
    }
//-------------------------------------------------------------------------------
 
    [HttpGet] 
    public async Task<IActionResult> Create(int? clienteId)
    {
        var veiculo = new Veiculo();

        if (clienteId.HasValue)
            veiculo.ClienteId = clienteId.Value;

        ViewBag.Clientes = await _db.Clientes.ToListAsync();
        return View(veiculo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Veiculo veiculo)
    {
        if(ModelState.IsValid)
        {
            _db.Veiculos.Add(veiculo);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        ViewBag.Clientes = await _db.Clientes.ToListAsync();
        return View(veiculo);
    }

//-------------------------------------------------------------------------------
 
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var veiculo = await _db.Veiculos.FindAsync(id);
        if (veiculo == null) return NotFound();

        ViewBag.Clientes = await _db.Clientes.ToListAsync();
        return View(veiculo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Veiculo veiculo)
    {
        if (ModelState.IsValid)
        {
            _db.Veiculos.Update(veiculo);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Clientes = await _db.Clientes.ToListAsync();
        return View(veiculo);
    }

//-------------------------------------------------------------------------------
 
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var veiculo = await _db.Veiculos.Include(v => v.Cliente).FirstOrDefaultAsync(v => v.Id == id);

        if (veiculo == null) return NotFound();
        return View(veiculo);
    }

//-------------------------------------------------------------------------------
 
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var veiculo = await _db.Veiculos.FindAsync(id);
        if (veiculo == null) return NotFound();
        return View(veiculo);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var veiculo = await _db.Veiculos.FindAsync(id);
        if (veiculo != null)
        {
            _db.Veiculos.Remove(veiculo);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    
//-------------------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> GetPorCliente(int clienteId)
    {
        var veiculos = await _db.Veiculos
            .Where(v => v.ClienteId == clienteId)
            .Select(v => new { id = v.Id, placa = v.Placa })
            .ToListAsync();

        return Json(veiculos);
    }    
}