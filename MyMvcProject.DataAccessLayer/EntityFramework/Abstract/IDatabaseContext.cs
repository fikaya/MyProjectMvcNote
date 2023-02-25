using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.IntermediateTable.Liked;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.DataAccessLayer.EntityFramework.Abstract
{
    public interface IDatabaseContext
    {
        DbSet<Note> Notes { get; set; }
        DbSet<MyProjectUser> MyProjectUsers { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Liked> Likes { get; set; }       
    }
}
