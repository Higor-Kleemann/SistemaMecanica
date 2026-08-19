using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class Estoque
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o produto")]
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")]
    public int Quantidade { get; set; }

    [StringLength(20)]
    public string? CodigoPrateleira { get; set; }

    [StringLength(20)]
    public string? CodigoNivel { get; set; }
}