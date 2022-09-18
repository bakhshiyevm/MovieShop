using DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class DataContext : DbContext
	{

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ;
            //modelBuilder.Entity<Contact>();
            //modelBuilder.Entity<Contact>().Ignore(x=>x.Value);
            //modelBuilder.Ignore<Contact>(); //or notmapped attr
        }

        public int SaveChanges(int userId = 1)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).UpdatedUser = userId;
                }
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).CreatedUser = userId;
                }

            }

            return base.SaveChanges();
        }
    }
}
