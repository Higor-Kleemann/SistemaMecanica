using Microsoft.EntityFrameworkCore;
using SistemaMecanica.Models;

namespace SistemaMecanica.Data
{
    public class AppDbContext : DbContext
    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }

    public DbSet<Caixa> Caixas { get; set; } = null!;
    public DbSet<Categoria> Categorias { get; set; } = null!;
    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Estoque> Estoques { get; set; } = null!;
    public DbSet<Fornecedor> Fornecedores { get; set; } = null!;
    public DbSet<ItemOS> ItensOS { get; set; } = null!;
    public DbSet<Mecanico> Mecanicos { get; set; } = null!;
    public DbSet<OrdemServico> OrdensServico { get; set; } = null!;
    public DbSet<UnidadeMedida> UnidadesMedida { get; set; } = null!;
    public DbSet<Veiculo> Veiculos { get; set; } = null!;
    }
}