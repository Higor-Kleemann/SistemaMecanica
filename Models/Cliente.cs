using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Selecione o tipo de pessoa")]
    [StringLength(2)]
    public string? TipoPessoa { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(150)]
    public string? NomeCompleto { get; set; }

    [StringLength(150)]
    public string? NomeFantasia { get; set; }

    [StringLength(14)]
    public string? CPF { get; set; }

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

    [Required(ErrorMessage = "O telefone é obrigatório")]
    [StringLength(20)]
    public string? Telefone { get; set; }

    public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}