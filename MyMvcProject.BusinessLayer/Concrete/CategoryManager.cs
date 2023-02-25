using MyMvcProject.Entities.Entity;
using System.Collections.Generic;
using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.DataAccessLayer.Abstract;
using System.Linq.Expressions;
using System;
using System.Linq;
using MyMvcProject.BusinessLayer.Concrete;

namespace MyMvcProject.BusinessLayer
{
    public class CategoryManager : BaseManager<Category>, ICategoryManager<Category>
    {
        public CategoryManager(IRepository<Category> repository) : base(repository)
        {
            //Burada bunu yapma sebebimizi açıklayalım.CategoryManager BaseManager ı inherit ediyor. Ve ben Ninject ile şu interface e şu nesneyi üret derken örnek verelim CategoryManager nesnesini ver diyorum.Şimdi bunu verdiğimiz de CategoryManagerın inherit ettiği BaseManagerın da bir constructor değeri verilmesi gerekiyor(Çünkü oda arkada newleniyor).Bende diyorum ki bu CategoryManager oluşurken onun constructor ına ne veriyorsam bunu git BaseManager ın constructorında da ver.
        }
        
    }
}
