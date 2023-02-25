using MyMvcProject.BusinessLayer.BusinessLayerResults;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Interface.ViewModelInterface;
using MyMvcProject.Entities.Messages.Obj;
using MyMvcProject.Entities.ViewModels;

namespace MyMvcProject.BusinessLayer.ControllersOperation.Abstract
{
    public interface IHomeOperation
    {
        IHomeIndexViewModel CategoryList();
        IHomeIndexViewModel NoteList(int? id = null);
        IHomeIndexViewModel NoteOrderByDescending();
    }
}
