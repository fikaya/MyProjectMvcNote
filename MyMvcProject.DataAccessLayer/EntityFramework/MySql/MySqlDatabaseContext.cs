using MyMvcProject.DataAccessLayer.EntityFramework.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.IntermediateTable.Liked;
using System.Data.Entity;

namespace MyMvcProject.DataAccessLayer.EntityFramework.MySql
{
    public class MySqlDatabaseContext : DbContext,IMySqlDatabaseContext
    {
        public MySqlDatabaseContext()
        {
            //Bu veritabanı oluşurken git şu başlatıcı olarak IDatabaseInitializer i implement eden bir classın referansını ver demiş olduk.Class olarak MyInitializer verdik ama bu class dediğimiz interface i implement etmiyor . Bu peki nasıl oldu. Çünkü MyInitializer CreateDatabaseIfNotExists classını inherit ediyor. Ve CreateDatabaseIfNotExists de IDatabaseInitializer i implement ediyor. Sonuçta nesnemizin içinde interface ile ilgili bir özellik var.
            Database.SetInitializer(new MyInitializer());
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<MyProjectUser> MyProjectUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }
      
    }
   
}
