using Microsoft.EntityFrameworkCore;

namespace TempooERP.Infrastructure;

public interface IModuleModelBuilder
{
    void Configure(ModelBuilder modelBuilder);
}
