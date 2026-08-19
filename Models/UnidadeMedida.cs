using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class UnidadeMedida
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(20)]
    public string? Nome { get; set; }

    [StringLength(100)]
    public string? Descricao { get; set; }
}