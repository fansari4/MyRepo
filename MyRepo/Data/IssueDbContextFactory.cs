using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using MyRepo.Data;

public class ModelDbContextFactory : IDesignTimeDbContextFactory<ModelDbContext>
{
    public ModelDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ModelDbContext>();
        optionsBuilder.UseSqlServer("Server=database-2.c4vvbylz92zk.us-east-1.rds.amazonaws.com;Database=main;TrustServerCertificate=True;Persist Security Info=True;User Id=admin;Password=Johnwick2;");

        return new ModelDbContext(optionsBuilder.Options);
    }
}