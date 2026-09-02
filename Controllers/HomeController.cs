using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMecanica.Data;
using SistemaMecanica.Models;

namespace SistemaMecanica.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var hoje = DateTime.Now;
        var inicioMes = new DateTime(hoje.Year, hoje.Month, 1);

        var viewModel = new DashboardViewModel
        {
            OsAbertas = await _db.OrdensServico.CountAsync(o => o.Status == "Aberto"),
            OsEmAndamento = await _db.OrdensServico.CountAsync(o => o.Status == "Em andamento"),
            OsConcluidas = await _db.OrdensServico.CountAsync(o => o.Status == "Concluído"),
            OsEntregues = await _db.OrdensServico.CountAsync(o => o.Status == "Entregue"),

            FaturamentoMesAtual = await _db.Caixas
                .Where(c => c.Tipo == "Entrada" && c.DataRegistro >= inicioMes)
                .SumAsync(c => (decimal?)c.Valor) ?? 0,

            TotalClientes = await _db.Clientes.CountAsync(),
            TotalVeiculos = await _db.Veiculos.CountAsync(),

            ProdutosEstoqueBaixo = await _db.Estoques
                .Include(e => e.Produto)
                .Where(e => e.Quantidade <= 5)
                .ToListAsync()
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}