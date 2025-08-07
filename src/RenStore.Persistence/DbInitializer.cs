namespace RenStore.Persistence
{
    public class DbInitializer
    {
        public static void Initializer(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}