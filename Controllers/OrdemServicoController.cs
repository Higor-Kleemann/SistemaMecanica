using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class OrdemServicoController : Controller
{
    private readonly AppDbContext _db;
    public OrdemServicoController(AppDbContext db)
    {
        _db = db;
    }

//----------------------------------------------------------------

    public async Task<IActionResult> Index()
    {
        var ordens = await _db.OrdensServico
            .Include(o => o.Cliente)
            .Include(o => o.Veiculo)
            .Include(o => o.Mecanico)
            .ToListAsync();
            return View(ordens);
    }

//------------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Clientes = await _db.Clientes.ToListAsync();
        ViewBag.Veiculos = await _db.Veiculos.ToListAsync();
        ViewBag.Mecanicos = await _db.Mecanicos.ToListAsync();
        ViewBag.Produtos = await _db.Produtos.ToListAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create (OrdemServico ordemServico)
    {
        if (ModelState.IsValid)
        {
            ordemServico.DataEntrada = DateTime.Now;
            ordemServico.Status = "Aberto";
            ordemServico.ValorTotal = 0;
            _db.OrdensServico.Add(ordemServico);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Clientes = await _db.Clientes.ToListAsync();
        ViewBag.Veiculos = await _db.Veiculos.ToListAsync();
        ViewBag.Mecanicos = await _db.Mecanicos.ToListAsync();
        ViewBag.Produtos = await _db.Produtos.ToListAsync();
        return View(ordemServico);
    }

//-----------------------------------------------------------------------------------

    [HttpGet]    
    public async Task<IActionResult> Edit(int id)
    {
        var ordem = await _db.OrdensServico.Include(o => o.Itens).FirstOrDefaultAsync(o => o.Id == id);

        if(ordem == null) return NotFound();

        ViewBag.Clientes = await _db.Clientes.ToListAsync();
        ViewBag.Veiculos = await _db.Veiculos.ToListAsync();
        ViewBag.Mecanicos = await _db.Mecanicos.ToListAsync();
        ViewBag.Produtos = await _db.Produtos.ToListAsync();

        return View(ordem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, OrdemServico ordemServico)
    {
        if (id != ordemServico.Id) return NotFound();

        if(ModelState.IsValid)
        {
            _db.OrdensServico.Update(ordemServico);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Clientes = await _db.Clientes.ToListAsync();
        ViewBag.Veiculos = await _db.Veiculos.ToListAsync();
        ViewBag.Mecanicos = await _db.Mecanicos.ToListAsync();
        ViewBag.Produtos = await _db.Produtos.ToListAsync();
        return View(ordemServico);
    }

//---------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var ordem = await _db.OrdensServico
            .Include(o => o.Cliente)
            .Include(o => o.Veiculo)
            .Include(o => o.Mecanico)
            .Include(o => o.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (ordem == null) return NotFound();

        ViewBag.Produtos = await _db.Produtos.ToListAsync();
        return View(ordem);
    }

//----------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var ordem = await _db.OrdensServico
            .Include(o => o.Cliente)
            .Include(o => o.Veiculo)
            .Include(o => o.Mecanico)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (ordem == null) return NotFound();
        return View(ordem);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ordem = await _db.OrdensServico.FindAsync(id);
        if (ordem != null)
        {
            _db.OrdensServico.Remove(ordem);
            await _db.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

//------------------------------------------------------------------------

[HttpPost]
public async Task<IActionResult> AtualizarStatus(int id, string status)
{
    var ordem = await _db.OrdensServico
        .Include(o => o.Itens)
        .FirstOrDefaultAsync(o => o.Id == id);

    if (ordem == null) return NotFound();

    ordem.Status = status;

    if (status == "Concluído" || status == "Entregue")
    {
        ordem.DataSaida = DateTime.Now;

        // verifica se já existe registro no caixa para essa OS
        var caixaExistente = await _db.Caixas
            .FirstOrDefaultAsync(c => c.OrdemServicoId == id);

        if (caixaExistente == null)
        {
            var caixa = new Caixa
            {
                OrdemServicoId = id,
                Tipo = "Entrada",
                Valor = ordem.ValorTotal,
                DataRegistro = DateTime.Now,
                Descricao = $"Pagamento da OS #{id}"
            };
            _db.Caixas.Add(caixa);
        }
    }

    await _db.SaveChangesAsync();
    return RedirectToAction(nameof(Details), new { id });
}

}