using MyMvcProject.DataAccessLayer.EntityFramework.Abstract;
using MyMvcProject.DataAccessLayer.EntityFramework.Abstract.Repository;
using MyMvcProject.DataAccessLayer.EntityFramework.Base;

namespace MyMvcProject.DataAccessLayer.EntityFramework.MySql
{
    //Repository Pattern<T>
    public class MySqlRepository<T> : BaseRepository<T,MySqlDatabaseContext>,IMySqlRepository where T : class
    {
        
    }
}
