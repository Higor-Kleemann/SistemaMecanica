namespace SistemaMecanica.Models;

public class OrdemServico
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public int VeiculoId { get; set; }
    public Veiculo? Veiculo { get; set; }
    public int MecanicoId { get; set; }
    public Mecanico? Mecanico { get; set; }
    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public string? Status { get; set; }
    public decimal ValorTotal { get; set; }
    public string? Observacao { get; set; }
    public List<ItemOS> Itens { get; set; } = new List<ItemOS>();
}