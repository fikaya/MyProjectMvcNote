using MyMvcProject.DataAccessLayer.EntityFramework.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.IntermediateTable.Liked;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.DataAccessLayer.EntityFramework.MsSql
{
    public class MsSqlDatabaseContext : DbContext, IMsSqlDatabaseContext
    {
        public MsSqlDatabaseContext()
        {
            //Bu veritabanı oluşurken git şu başlatıcı olarak IDatabaseInitializer i implement eden bir classın referansını ver demiş olduk.Class olarak MyInitializer verdik ama bu class dediğimiz interface i implement etmiyor . Bu peki nasıl oldu. Çünkü MyInitializer CreateDatabaseIfNotExists classını inherit ediyor. Ve CreateDatabaseIfNotExists de IDatabaseInitializer i implement ediyor. Sonuçta nesnemizin içinde interface ile ilgili bir özellik var.
            Database.SetInitializer(new MyInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasMany(n => n.Comments)
                .WithOptional(c => c.Note)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Note>()
                .HasMany(n => n.Likes)
                .WithOptional(c => c.Note)
                .WillCascadeOnDelete(true);
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<MyProjectUser> MyProjectUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }

    }

}
