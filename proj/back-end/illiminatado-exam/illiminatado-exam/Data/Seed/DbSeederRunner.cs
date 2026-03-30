namespace illiminatado_exam.Data.Seed
{
    public class DbSeederRunner
    {
        public static void Run(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            DbSeeder.Seed(context);
        }
    }
}
