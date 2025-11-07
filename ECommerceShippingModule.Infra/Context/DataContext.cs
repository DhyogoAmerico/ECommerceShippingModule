using ECommerceShippingModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceShippingModule.Infra.Context;

public class DataContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>().HasKey(o => o.Id);
    }
}
