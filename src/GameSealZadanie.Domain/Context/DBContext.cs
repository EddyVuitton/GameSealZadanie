using GameSealZadanie.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameSealZadanie.Domain.Context;

public class DBContext(DbContextOptions<DBContext> options) : DbContext(options)
{
    public DbSet<Badge> Badge => Set<Badge>();
    public DbSet<Image> Image => Set<Image>();
    public DbSet<Language> Language => Set<Language>();
    public DbSet<Link> Link => Set<Link>();
    public DbSet<Price> Price => Set<Price>();
    public DbSet<Product> Product => Set<Product>();
    public DbSet<Region> Region => Set<Region>();
}