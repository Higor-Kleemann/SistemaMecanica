namespace SistemaMecanica.Models;

public class Veiculo
{
    public int Id { get; set; }
    public string? Placa { get; set; }
    public string? Cor { get; set; }
    public string? AnoModelo { get; set; }
    public string? Marca { get; set; }
    public float KM { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
}