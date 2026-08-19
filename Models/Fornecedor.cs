using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class Fornecedor
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome fantasia é obrigatório")]
    [StringLength(150)]
    public string? NomeFantasia { get; set; }

    [Required(ErrorMessage = "O CNPJ é obrigatório")]
    [StringLength(18)]
    public string? CNPJ { get; set; }

    [StringLength(9)]
    public string? CEP { get; set; }

    [StringLength(100)]
    public string? Bairro { get; set; }

    [StringLength(150)]
    public string? Rua { get; set; }

    [StringLength(10)]
    public string? Numero { get; set; }

    [StringLength(100)]
    public string? Cidade { get; set; }

    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [StringLength(150)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? Telefone { get; set; }
}