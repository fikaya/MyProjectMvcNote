using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.DataAccessLayer.Abstract;
using MyMvcProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.BusinessLayer.Concrete
{
    public abstract class BaseManager<T> : IBaseManager<T> where T : class
    {
        IRepository<T> _repository;
        public BaseManager(IRepository<T> repository)
        {
            _repository = repository;
        }
        public List<T> Get()
        {
            var list = _repository.List();
            return list;
        }
        public List<T> Get(Expression<Func<T, bool>> where)
        {
            var list = _repository.List(where);
            return list;
        }
        public IQueryable<T> GetQueryable()
        {
            return _repository.ListQueryable();
        }
        public IEnumerable<T> GetEnumerable()
        {
            return _repository.ListEnumerable();
        }
        public List<T> GetCollection(params string[] collectionNames)
        {
            var list = _repository.ListCollection(collectionNames);
            return list;
        }
        public List<T> GetReference(params string[] referenceNames)
        {
            var list = _repository.ListReference(referenceNames);
            return list;
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            var entity = _repository.Find(where);
            return entity;
        }
        public List<T> GetCollection(Expression<Func<T, bool>> where, params string[] collectionNames)
        {
            var list = _repository.ListCollection(where, collectionNames);
            return list;
        }
        public List<T> GetReference(Expression<Func<T, bool>> where, params string[] referenceNames)
        {
            var list = _repository.ListReference(where, referenceNames);
            return list;
        }
        public int Insert(T obj)
        {
           return _repository.Insert(obj);
        }

        public int Update(T obj)
        {
           return _repository.Update(obj);
        }
        public virtual int Delete(T obj)
        {
            return _repository.Delete(obj);
        }
    }
}
