namespace SistemaMecanica.Models;

public class Estoque
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public int Quantidade { get; set; }
    public string? CodigoPrateleira { get; set; }
    public string? CodigoNivel { get; set; }
}