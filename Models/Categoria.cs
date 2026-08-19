using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(60)]
    public string? Nome { get; set; }
}