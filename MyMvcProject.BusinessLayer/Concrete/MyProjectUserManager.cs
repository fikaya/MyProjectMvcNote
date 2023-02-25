using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.DataAccessLayer.Abstract;
using MyMvcProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.BusinessLayer.Concrete
{
    public class MyProjectUserManager:BaseManager<MyProjectUser>,IMyProjectManager<MyProjectUser> 
    {
        public MyProjectUserManager(IRepository<MyProjectUser> repository):base(repository)
        {

        }
    }
}
