
namespace Example.Library.DataAccessSQL
{
    public interface IAppDbContextFactory<out TContext>
        where TContext : AppDbContext
    {
        TContext Create();
    }
}
