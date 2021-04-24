using Microsoft.EntityFrameworkCore;
using UnitOfWorkAsync.Models;

namespace UnitOfWorkAsync.Context
{
    public class Db : DbContext
    {
        public DbSet<Table> Tables { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=EDU-WAL-PRA-C10\SQLEXPRESS;Database=TablesBC;Integrated Security=True");
        }
       /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
           modelBuilder.Entity<Table>()
               .Property(a => a.RowVersion)
               .HasColumnType("timestamp")
               .ValueGeneratedOnAddOrUpdate()
               .IsConcurrencyToken();
        }
      */
    }
}
/*
 Microsoft.EntityFrameworkCore

 Windows Authorisation
 add-migration TablesBC
 update-database –verbose
 */


