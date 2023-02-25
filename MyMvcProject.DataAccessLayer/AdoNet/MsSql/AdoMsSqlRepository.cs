using MyMvcProject.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.DataAccessLayer.AdoNet.MsSql
{
    public class AdoMsSqlRepository<T>:IRepository<T> where T : class
    {
        public AdoMsSqlRepository()
        {

        }

        public int Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public int Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public List<T> List()
        {
            throw new NotImplementedException();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public List<T> ListCollection(params string[] collectionNames)
        {
            throw new NotImplementedException();
        }

        public List<T> ListCollection(Expression<Func<T, bool>> where, params string[] collectionNames)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ListEnumerable()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> ListQueryable()
        {
            throw new NotImplementedException();
        }

        public List<T> ListReference(params string[] referenceNames)
        {
            throw new NotImplementedException();
        }

        public List<T> ListReference(Expression<Func<T, bool>> where, params string[] referenceNames)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public int Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
