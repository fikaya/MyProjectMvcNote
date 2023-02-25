using MyMvcProject.DataAccessLayer.EntityFramework.Abstract.Repository;
using MyMvcProject.DataAccessLayer.EntityFramework.Base;

namespace MyMvcProject.DataAccessLayer.EntityFramework.MsSql
{
    //Repository Pattern<T>
    public class MsSqlRepository<T> : BaseRepository<T,MsSqlDatabaseContext>,IMsSqlRepository where T : class
    {
      
    }
}
