using MyMvcProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMvcProject.WebApp.Models
{
    public static class CurrentSession
    {
        public static MyProjectUser User
        {
            get
            {
                return Get<MyProjectUser>("User");
            }
        }
        public static void Set<T>(string key, T obj)
        {
            HttpContext.Current.Session[key] = obj;
        }
        public static T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return (T)HttpContext.Current.Session[key];
            }

            return default(T);
            //DEFAULT olarak dön, şu demek . Eğer T tip olarak class ise null döner int ise 0 döner gibi vb.
        }
        public static void Remove(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}