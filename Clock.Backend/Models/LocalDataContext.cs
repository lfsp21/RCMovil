namespace Clock.Backend.Models
{
    using Clock.Domain.Models;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Clock.Common.Models.Register> Registers { get; set; }
    }
}