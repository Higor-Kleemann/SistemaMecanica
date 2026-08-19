using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class Servico
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
    public decimal? Preco { get; set; }

    [StringLength(300)]
    public string? Descricao { get; set; }
}