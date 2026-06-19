namespace SistemaMecanica.Models;

public class Produto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public int UnidadeMedidaId { get; set; }
    public UnidadeMedida? UnidadeMedida { get; set; }
}