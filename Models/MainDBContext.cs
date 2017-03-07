using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class MainDBContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonRole> PersonRole { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<ActivityType> ActivityType { get; set; }
        public DbSet<ActivityStatus> ActivityStatus { get; set; }
        public DbSet<Contribution> Contribution { get; set; }
        public DbSet<ContributionType> ContributionType { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
         {
             modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
         }

    }
}