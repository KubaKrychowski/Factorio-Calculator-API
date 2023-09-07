using Domain.Consts;
using Domain.Entities;
using Domain.Maps;
using Infrastructure.EntitiesUtils;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data;

public class DataContext : DbContext
{
    public DbSet<ItemCategory> ItemCategory => Set<ItemCategory>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<RecipeIngredient> Ingregients => Set<RecipeIngredient>();
    public DbSet<Recipe> Recipes => Set<Recipe>();

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityAlwaysColumns();

        modelBuilder.ApplyConfiguration(new ItemMap());
        modelBuilder.ApplyConfiguration(new ItemCategoryMap());
        modelBuilder.ApplyConfiguration(new RecipeIngredientMap());
        modelBuilder.ApplyConfiguration(new RecipeMap());
    }
}