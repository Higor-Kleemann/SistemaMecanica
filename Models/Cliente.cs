namespace SistemaMecanica.Models;

public class Cliente
{
    public int Id { get; set; }
    public string? TipoPessoa { get; set; }
    public string? NomeCompleto { get; set; }
    public string? NomeFantasia { get; set; }
    public string? CPF { get; set; }
    public string? CNPJ { get; set; }
    public string? CEP { get; set; }
    public string? Bairro { get; set; }
    public string? Rua { get; set; }
    public string? Numero { get; set; }
    public string? Cidade { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}
