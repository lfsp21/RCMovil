namespace Clock.Domain.Models
{
    using Clock.Common.Models;
    using System.Data.Entity;
   
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Register> Registers { get; set; }

  
    }
}
