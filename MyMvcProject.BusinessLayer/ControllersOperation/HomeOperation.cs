using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.BusinessLayer.BusinessLayerResults;
using MyMvcProject.BusinessLayer.Concrete;
using MyMvcProject.BusinessLayer.ControllersOperation.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Interface.ViewModelInterface;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcProject.BusinessLayer.ControllersOperation
{
    public class HomeOperation : IHomeOperation
    {
        private IBaseManager<Category> _categoryManager;
        private IBaseManager<Note> _noteManager;
        private IHomeIndexViewModel _viewModel;

        //Burada Ninject ile ilgili bir durum var. Mesela ilk başta sayfaya istek attım attım ve Index sayfası doldu. Ve Index sayfası içinde _viewModel deki Notes ve Categories listelerime dolunm yapmış oldum.Ben sayfaya bir daha istek attığımda listelerimin içinin yine dolu olduğunu görüyorum. Buradan şunu çıkarabiliriz. Normalde sayfaya ikinci bir kez istek atıldığında nesneler tekrar oluşturulurdu.Ama Ninject ile ilk istekte çeşitli Interfacelere karşı oluşturulan nesnelerimin içleri ve nesne işaretlemesi sabit kalıyor. Bu hatayı Index sayfası her yenilendiiğinde kategorilerin listelendiği barın beşer beşer arttığını görerek fark ettim.Aşağıdakileri incelmemiz gerek.
        //InSingletonScope()
        //Indicates that only a single instance of the binding should be created and then should be reused for all subsequent requests
        //Bağlamanın yalnızca tek bir örneğinin oluşturulması gerektiğini ve ardından sonraki tüm istekler için yeniden kullanılması gerektiğini belirtir.
        //InTransientScope
        //Indicates that instance activated via the binding should not re-used,nor have their lifecycle managed by Ninject
        //Bağlama aracılığıyla etkinleştirilen eşgörünümün yeniden kullanılmaması veya yaşam döngülerinin Ninject tarafından yönetilmesi gerektiğini belirtir.
        public HomeOperation
            (
            IBaseManager<Category> categoryManager,
            IBaseManager<Note> noteManager,
             IHomeIndexViewModel viewModel
          )
        {
            _categoryManager = categoryManager;
            _noteManager = noteManager;
            _viewModel = viewModel;
        }
        public IHomeIndexViewModel CategoryList()
        {
            var categoryList = _categoryManager.Get();

            foreach (var category in categoryList)
            {
                _viewModel.Categories.Add(category);
            }
            return _viewModel;
        }
        public IHomeIndexViewModel NoteList(int? id = null)
        {
            List<Note> noteList;

            if (id != null)
            {
                noteList = _noteManager.GetReference(x => x.CategoryID == id && x.IsDraft == false, "MyProjectUser");
            }
            else
            {
                noteList = _noteManager.GetReference("MyProjectUser");
            }

            noteList = noteList.OrderByDescending(x => x.ModifiedOn).ToList();

            foreach (var item in noteList)
            {
                _viewModel.Notes.Add(item);
            }
            return _viewModel;
        }
        public IHomeIndexViewModel NoteOrderByDescending()
        {
            var noteList = _noteManager.GetReference(x => x.IsDraft == false, "MyProjectUser");
            var list = noteList.OrderByDescending(x => x.LikeCount);

            foreach (var item in list)
            {
                _viewModel.Notes.Add(item);
            }
            return _viewModel;
        }

    }
}
