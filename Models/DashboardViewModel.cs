namespace SistemaMecanica.Models;

public class DashboardViewModel
{
    public int OsAbertas { get; set; }
    public int OsEmAndamento { get; set; }
    public int OsConcluidas { get; set; }
    public int OsEntregues { get; set; }

    public decimal FaturamentoMesAtual { get; set; }

    public int TotalClientes { get; set; }
    public int TotalVeiculos { get; set; }

    public List<Estoque> ProdutosEstoqueBaixo { get; set; } = new List<Estoque>();
}