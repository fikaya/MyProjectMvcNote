using MyMvcProject.BusinessLayer.BusinessLayerResults;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.BusinessLayer.ControllersOperation.Abstract
{
    public interface ILoginOperation 
    {
        IBusinessBaseLayerResult<MyProjectUser> RegisterUser(RegisterViewModel registerViewModel);
        IBusinessBaseLayerResult<MyProjectUser> RegisterUser(MyProjectUser myProjectUser);
        IBusinessBaseLayerResult<MyProjectUser> LoginUser(LoginViewModel registerViewModel);
        IBusinessBaseLayerResult<MyProjectUser> ActivateUser(Guid ID);
        IBusinessBaseLayerResult<MyProjectUser> GetUserByID(int? id);
        IBusinessBaseLayerResult<MyProjectUser> EditUser(MyProjectUser myProjectUser);
        IBusinessBaseLayerResult<MyProjectUser> RemoveUserByID(int id);

    }
}
