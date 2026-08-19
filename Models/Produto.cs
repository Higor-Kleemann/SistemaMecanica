using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(150)]
    public string? Nome { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
    public decimal Preco { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione a categoria")]
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione a unidade de medida")]
    public int UnidadeMedidaId { get; set; }
    public UnidadeMedida? UnidadeMedida { get; set; }
}