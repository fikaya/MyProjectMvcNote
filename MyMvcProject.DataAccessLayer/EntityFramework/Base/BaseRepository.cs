using MyMvcProject.CommonLayer;
using MyMvcProject.DataAccessLayer.Abstract;
using MyMvcProject.DataAccessLayer.EntityFramework.Abstract;
using MyMvcProject.DataAccessLayer.EntityFramework.MsSql;
using MyMvcProject.Entities.Base.EntityBase;
using MyMvcProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.DataAccessLayer.EntityFramework.Base
{
    public abstract class BaseRepository<T, U> : IRepository<T>
        where T : class
        where U : DbContext, new()
    {
        private U _dbContext = new U();
        private DbSet<T> _objectSet;
        public BaseRepository()
        {
            //Set<T>() metodunun generic parametresine kullanıcı int gibi bir değer göndermemeli. Buraya sadece entity isimlerim gelmeli.Onun için bir constraint uygulayacağım. where T:Class diyerek bir kısıtlama getirdim.
            //Burada sonuç olarak Set<T>() metodundan bir DbSet<T> dönecek.
            _objectSet = _dbContext.Set<T>();
        }
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public List<T> List(Expression<Func<T, bool>> where)
        {
            var list = _objectSet.Where(where).ToList();
            return list;
        }

        //Enumerable işlerini bellekte yapar,Queryable ise database içinde.
        //Ben bunun bir sorgu olarak kalmasını istersem.Ve üstüne çeşitli sorgular eklemek istersem aşağıdaki iki metodu kullanabilirim. (Order by gibi)
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }
        public IEnumerable<T> ListEnumerable()
        {
            return _objectSet.AsEnumerable<T>();
        }
        public List<T> ListCollection(Expression<Func<T, bool>> where, params string[] collectionNames)
        {
            List<T> list;
            if (where != null)
            {
                list = _objectSet.Where(where).ToList();
            }
            else
            {
                list = _objectSet.ToList();
            }
            foreach (var item in list)
            {
                for (int i = 0; i < collectionNames.Length; i++)
                {
                    _dbContext.Entry(item).Collection(collectionNames[i]).Load();
                }
            }
            return list;
        }
        public List<T> ListCollection(params string[] collectionNames)
        {
            var list = _objectSet.ToList();
            foreach (var item in list)
            {
                for (int i = 0; i < collectionNames.Length; i++)
                {
                    _dbContext.Entry(item).Collection(collectionNames[i]).Load();
                }
            }
            return list;
        }
        public List<T> ListReference(Expression<Func<T, bool>> where, params string[] referenceNames)
        {
            List<T> list;
            if (where != null)
            {
                list = _objectSet.Where(where).ToList();
            }
            else
            {
                list = _objectSet.ToList();
            }
            foreach (var item in list)
            {
                for (int i = 0; i < referenceNames.Length; i++)
                {
                    _dbContext.Entry(item).Reference(referenceNames[i]).Load();
                }
            }
            return list;
        }
        public List<T> ListReference(params string[] referenceNames)
        {
            var list = _objectSet.ToList();
            foreach (var item in list)
            {
                for (int i = 0; i < referenceNames.Length; i++)
                {
                    _dbContext.Entry(item).Reference(referenceNames[i]).Load();
                }
            }
            return list;
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
        public int Insert(T obj)
        {
            var myEntityBase = obj as MyEntityBase<int>;
            var dateTimeNow = DateTime.Now;
            if (myEntityBase != null)
            {
                myEntityBase.ModifiedOn = dateTimeNow;
                myEntityBase.CreatedOn = dateTimeNow;
                myEntityBase.ModifiedUsername = App.Common.GetUserName();
            }
            _objectSet.Add(obj);
            return Save();
        }
        public int Update(T obj)
        {
            var myEntityBase = obj as MyEntityBase<int>;
            var dateTimeNow = DateTime.Now;
            if (myEntityBase != null)
            {
                myEntityBase.ModifiedOn = dateTimeNow;
                myEntityBase.ModifiedUsername = App.Common.GetUserName();
            }

            var entries = _dbContext.ChangeTracker.Entries();

            foreach (var item in entries)
            {
                item.State = EntityState.Detached;
            }

            _dbContext.Entry(obj).State = EntityState.Modified;
 
            return Save();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }

}
