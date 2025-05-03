/*namespace RenStore.Persistence.Test.Common;

public abstract class TestCommandBase : IDisposable
{
    protected readonly ApplicationDbContext context;

    public TestCommandBase()
    {
        this.context = DbContextFactory.Create();
    }

    public void Dispose()
    {
        DbContextFactory.Destroy(this.context);
    }
}*/