using System.ComponentModel.DataAnnotations;

namespace SistemaMecanica.Models;

public class Veiculo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A placa é obrigatória")]
    [StringLength(8)]
    public string? Placa { get; set; }

    [StringLength(30)]
    public string? Cor { get; set; }

    [StringLength(9)]
    public string? AnoModelo { get; set; }

    [Required(ErrorMessage = "A marca é obrigatória")]
    [StringLength(60)]
    public string? Marca { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "A quilometragem não pode ser negativa")]
    public float KM { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione o cliente")]
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
}