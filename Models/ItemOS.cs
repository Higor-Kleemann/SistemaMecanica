namespace SistemaMecanica.Models;

public class ItemOS
{
    public int Id { get; set; }
    public int OrdemServicoId { get; set; }
    public OrdemServico? OrdemServico { get; set; }
    public int? ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }

    public int? ServicoId { get; set; }
    public Servico? Servico { get; set; }
    public string? Descricao{ get; set; }
}