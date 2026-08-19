using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class Mecanico
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(150)]
    public string? NomeCompleto { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório")]
    [StringLength(14)]
    public string? CPF { get; set; }

    [StringLength(5)]
    public string? TipoSanguineo { get; set; }

    [StringLength(20)]
    public string? TelefonePessoal { get; set; }

    [StringLength(20)]
    public string? TelefoneResponsavel { get; set; }

    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [StringLength(150)]
    public string? Email { get; set; }
}