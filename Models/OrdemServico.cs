using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class OrdemServico
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o cliente")]
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o veículo")]
    public int VeiculoId { get; set; }
    public Veiculo? Veiculo { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o mecânico")]
    public int MecanicoId { get; set; }
    public Mecanico? Mecanico { get; set; }

    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }
    public string? Status { get; set; }
    public decimal ValorTotal { get; set; }

    [StringLength(500)]
    public string? Observacao { get; set; }

    public List<ItemOS> Itens { get; set; } = new List<ItemOS>();
}