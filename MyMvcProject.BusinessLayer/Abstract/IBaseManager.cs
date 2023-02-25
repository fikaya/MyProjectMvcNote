using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyMvcProject.BusinessLayer.Abstract
{
    public interface IBaseManager<T>
    {
        List<T> Get();
        List<T> Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetQueryable();
        IEnumerable<T> GetEnumerable();
        List<T> GetCollection(params string[] collectionNames);
        List<T> GetCollection(Expression<Func<T, bool>> where, params string[] collectionNames);
        List<T> GetReference(params string[] referenceNames);
        List<T> GetReference(Expression<Func<T, bool>> where, params string[] referenceNames);
        T Find(Expression<Func<T, bool>> where);
        int Insert(T obj);
        int Update(T obj);
        int Delete(T obj);
    }
}
