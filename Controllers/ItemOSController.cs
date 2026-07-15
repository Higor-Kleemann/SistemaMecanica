using Microsoft.AspNetCore.Mvc;
using SistemaMecanica.Data;
using SistemaMecanica.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaMecanica.Controllers;

public class ItemOSController : Controller
{
    private readonly AppDbContext _db;

    public ItemOSController(AppDbContext db)
    {
        _db = db;
    }

//-----------------------------------------------------------

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar(ItemOS item)
    {
        if (ModelState.IsValid)
        {
            //busca o preço atual do produto
            var produto = await _db.Produtos.FindAsync(item.ProdutoId);
            if (produto == null) return NotFound();

            item.PrecoUnitario = produto.Preco;

            _db.ItensOS.Add(item);
            await _db.SaveChangesAsync();

            //recalcula o valor total da OS
            await RecalcularValorTotal(item.OrdemServicoId);

            // muda status para Em andamento ao adicionar primeiro item
            var ordem = await _db.OrdensServico.FindAsync(item.OrdemServicoId);
            if (ordem != null && ordem.Status == "Aberto")
            {
                ordem.Status = "Em andamento";
                await _db.SaveChangesAsync();
            }
        }

        return RedirectToAction("Details", "OrdemServico", new { id = item.OrdemServicoId });
    }

//--------------------------------------------------------------------------------------------

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remover(int id, int ordemServicoId)
    {
        var item = await _db.ItensOS.FindAsync(id);
        if(item != null)
        {
            _db.ItensOS.Remove(item);
            await _db.SaveChangesAsync();

            //recalcula o valor total da OS
            await RecalcularValorTotal(ordemServicoId);
        }

        return RedirectToAction("Details", "OrdemServico", new { id = ordemServicoId });
    }

    private async Task RecalcularValorTotal(int ordemServicoId)
    {
        var ordem = await _db.OrdensServico.Include(o => o.Itens).FirstOrDefaultAsync(o => o.Id == ordemServicoId);

        if(ordem != null)
        {
            ordem.ValorTotal = ordem.Itens.Sum(i => i.Quantidade * i.PrecoUnitario );
            await _db.SaveChangesAsync();
        }
    }
}