using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using MyRepo.Data;

public class ModelDbContextFactory : IDesignTimeDbContextFactory<ModelDbContext>
{
    public ModelDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ModelDbContext>();
        optionsBuilder.UseSqlServer("Data Source=sql.bsite.net\\MSSQL2016;TrustServerCertificate=True;Persist Security Info=True;User ID=fansari_;Password=falah123");

        return new ModelDbContext(optionsBuilder.Options);
    }
}