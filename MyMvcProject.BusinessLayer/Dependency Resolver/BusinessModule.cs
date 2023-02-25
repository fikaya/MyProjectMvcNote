using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.BusinessLayer.BusinessLayerResults;
using MyMvcProject.BusinessLayer.Concrete;
using MyMvcProject.BusinessLayer.ControllersOperation;
using MyMvcProject.BusinessLayer.ControllersOperation.Abstract;
using MyMvcProject.DataAccessLayer.Abstract;
using MyMvcProject.DataAccessLayer.EntityFramework.MsSql;
using MyMvcProject.DataAccessLayer.EntityFramework.MySql;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Interface.ViewModelInterface;
using MyMvcProject.Entities.IntermediateTable.Liked;
using MyMvcProject.Entities.Messages;
using MyMvcProject.Entities.Messages.Obj;
using MyMvcProject.Entities.ViewModels;
using MyMvcProject.Entities.ViewModels.Notify;
using Ninject.Modules;

namespace MyMvcProject.BusinessLayer.Dependency_Resolver
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryManager<Category>>().To<CategoryManager>().InTransientScope();

            Bind<INoteManager<Note>>().To<NoteManager>().InTransientScope();

            Bind<IBaseManager<Liked>>().To<LikeManager>().InTransientScope();

            Bind<IBaseManager<Comment>>().To<CommentManager>().InTransientScope();

            Bind<IBaseManager<Category>>().To<CategoryManager>().InTransientScope();

            Bind<IBaseManager<Note>>().To<NoteManager>().InTransientScope();

            Bind<IBaseManager<MyProjectUser>>().To<MyProjectUserManager>().InTransientScope();

            Bind<IRepository<Category>>().To<MsSqlRepository<Category>>().InTransientScope();

            Bind<IRepository<Note>>().To<MsSqlRepository<Note>>().InTransientScope();

            Bind<IRepository<Comment>>().To<MsSqlRepository<Comment>>().InTransientScope();

            Bind<IRepository<Liked>>().To<MsSqlRepository<Liked>>().InTransientScope();

            Bind<IRepository<MyProjectUser>>().To<MsSqlRepository<MyProjectUser>>().InTransientScope();

            Bind<IHomeIndexViewModel>().To<HomeIndexViewModel>().InTransientScope();

            Bind<IHomeOperation>().To<HomeOperation>().InTransientScope();

            Bind<ILoginOperation>().To<LoginOperation>().InTransientScope();

            Bind<IBusinessBaseLayerResult<MyProjectUser>>().To<BusinessLayerResult<MyProjectUser>>().InTransientScope();

            Bind<INotifyViewModel<MessageObj, ErrorViewModel>>().To<ErrorViewModel>().InTransientScope();

            Bind<INotifyViewModel<MessageObj, InformationViewModel>>().To<InformationViewModel>().InTransientScope();
            
            Bind<INotifyViewModel<MessageObj, WarningViewModel>>().To<WarningViewModel>().InTransientScope();
            
            Bind<INotifyViewModel<MessageObj, OKViewModel>>().To<OKViewModel>().InTransientScope();
 
        }
    }
}
