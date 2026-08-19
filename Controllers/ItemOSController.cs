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
            // busca o preço atual do produto
            var produto = await _db.Produtos.FindAsync(item.ProdutoId);
            if (produto == null) return NotFound();

            // verifica estoque disponível
            var estoque = await _db.Estoques
                .FirstOrDefaultAsync(e => e.ProdutoId == item.ProdutoId);

            if (estoque == null || estoque.Quantidade <= 0)
            {
                TempData["Erro"] = $"Produto '{produto.Nome}' não possui estoque disponível.";
                return RedirectToAction("Details", "OrdemServico", new { id = item.OrdemServicoId });
            }

            if (item.Quantidade > estoque.Quantidade)
            {
                TempData["Erro"] = $"Quantidade insuficiente em estoque. Disponível: {estoque.Quantidade} unidade(s) de '{produto.Nome}'.";
                return RedirectToAction("Details", "OrdemServico", new { id = item.OrdemServicoId });
            }

            item.PrecoUnitario = produto.Preco;

            _db.ItensOS.Add(item);
            await _db.SaveChangesAsync();

            // desconta do estoque
            estoque.Quantidade -= item.Quantidade;
            await _db.SaveChangesAsync();

            // busca a ordem uma única vez para recalcular e atualizar status
            var ordem = await _db.OrdensServico
                .Include(o => o.Itens)
                .FirstOrDefaultAsync(o => o.Id == item.OrdemServicoId);

            if (ordem != null)
            {
                ordem.ValorTotal = ordem.Itens.Sum(i => i.Quantidade * i.PrecoUnitario);

                if (ordem.Status == "Aberto")
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
        if (item != null)
        {
            // devolve ao estoque
            var estoque = await _db.Estoques
                .FirstOrDefaultAsync(e => e.ProdutoId == item.ProdutoId);

            if (estoque != null)
            {
                estoque.Quantidade += item.Quantidade;
            }

            _db.ItensOS.Remove(item);
            await _db.SaveChangesAsync();

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

    //--------------------------------------------------------------------------------------------

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdicionarServico(int ordemServicoId, int servicoId, decimal precoUnitario, string? descricao)
    {
        var servico = await _db.Servicos.FindAsync(servicoId);
        if (servico == null) return NotFound();

        var item = new ItemOS
        {
            OrdemServicoId = ordemServicoId,
            ServicoId = servicoId,
            Quantidade = 1,
            PrecoUnitario = precoUnitario,
            Descricao = descricao
        };

        _db.ItensOS.Add(item);
        await _db.SaveChangesAsync();

        // recalcula valor total e atualiza status
        var ordem = await _db.OrdensServico
            .Include(o => o.Itens)
            .FirstOrDefaultAsync(o => o.Id == ordemServicoId);

        if (ordem != null)
        {
            ordem.ValorTotal = ordem.Itens.Sum(i => i.Quantidade * i.PrecoUnitario);

            if (ordem.Status == "Aberto")
                ordem.Status = "Em andamento";

            await _db.SaveChangesAsync();
        }

        return RedirectToAction("Details", "OrdemServico", new { id = ordemServicoId });
    }
}