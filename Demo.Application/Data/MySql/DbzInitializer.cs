namespace Demo.Application.Data.MySql
{
    public static class DbzInitializer
    {
        public static void Initialize(DbzMySqlContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
