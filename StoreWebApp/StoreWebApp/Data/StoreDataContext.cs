using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StoreWebApp.Data
{
  public class StoreDataContext : DbContext
  {
    public StoreDataContext()
      : base("DefaultConnection")
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;

      Database.SetInitializer(
        new MigrateDatabaseToLatestVersion<StoreDataContext, StoreDataMigrationsConfiguration>()
        );
    }

    public DbSet<Topic> Topics { get; set; }
    public DbSet<Reply> Replies { get; set; }

    public DbSet<Group> Groups { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Detail> Details { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
  }
}