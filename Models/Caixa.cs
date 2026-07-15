namespace SistemaMecanica.Models;

public class Caixa
{
    public int Id { get; set; }
    public int? OrdemServicoId { get; set; }
    public OrdemServico? OrdemServico { get; set; }
    public string Tipo { get; set; } = string.Empty; // "Entrada" ou "Saída"
    public decimal Valor { get; set; }
    public DateTime DataRegistro { get; set; }
    public string? Descricao { get; set; }
}