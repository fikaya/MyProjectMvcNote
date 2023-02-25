using System.Net;
using System.Web.Mvc;
using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.BusinessLayer.ControllersOperation.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Messages;
using MyMvcProject.Entities.Messages.Enums;
using MyMvcProject.Entities.Messages.Obj;
using MyMvcProject.Entities.ViewModels.Notify;
using MyMvcProject.WebApp.Filters;

namespace MyMvcProject.WebApp.Controllers
{
    [Auth]
    [Exc]
    [AuthAdmin]
    public class MyProjectUserController : Controller
    {
        IBaseManager<MyProjectUser> _myProjectUser;
        ILoginOperation _loginOperation;
        INotifyViewModel<MessageObj, ErrorViewModel> _notificationErrorViewModel;
        INotifyViewModel<MessageObj, InformationViewModel> _notificationInfoViewModel;
        INotifyViewModel<MessageObj, OKViewModel> _notificationOKModel;
        public MyProjectUserController(
            IBaseManager<MyProjectUser> myProjectUser,
           ILoginOperation loginOperation,
             INotifyViewModel<MessageObj, ErrorViewModel> notificationErrorViewModel,
            INotifyViewModel<MessageObj, OKViewModel> notificationOKModel,
            INotifyViewModel<MessageObj, InformationViewModel> notificationInfoViewModel
            )
        {
            _myProjectUser = myProjectUser;
            _loginOperation = loginOperation;
            _notificationErrorViewModel = notificationErrorViewModel;
            _notificationOKModel = notificationOKModel;
            _notificationInfoViewModel = notificationInfoViewModel;
        }
        // GET: MyProjectUser
        public ActionResult Index()
        {
            return View(_myProjectUser.Get());
        }

        // GET: MyProjectUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var myProjectUser = _myProjectUser.Find(x => x.ID == id);

            if (myProjectUser == null)
            {
                return HttpNotFound();
            }

            return View(myProjectUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyProjectUser myProjectUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                //Eğer bu şartın içine girilmiş ise modelimizde herhangi bir hata yoktur.
                //Şimdi ben bazı kontroller yaparak custom hatalar da ekleyebilirim.

                var businessLayerResult = _loginOperation.RegisterUser(myProjectUser);

                if (businessLayerResult.MessageObjList.Count > 0)
                {
                    foreach (var item in businessLayerResult.MessageObjList)
                    {
                        //Bu şekilde bir ERROR eklendiğinde ModelState in value değerlerine bir null değer ekleniyor ama hata olarak da buradaki item veriliyor.Ve bu hata sayfada property hatası olmadığı için ValidationSummary içinde gösteriliyor.
                        ModelState.AddModelError("", item.Message);
                    }
                    return View(myProjectUser);
                }
                else
                {
                    _notificationOKModel.Items.Add(new MessageObj()
                    {
                        MessageCode =
                   MessageCode.RegisterSuccess,
                        Message = "Başarılı Bir Şekilde Kullanıcı Oluşturuldu."
                    });
                    _notificationOKModel.Title = "Kayıt Başarılı";
                    return View("OK", _notificationOKModel);
                }
            }
            else
            {
                return View(myProjectUser);
            }
        }

        // GET: MyProjectUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyProjectUser myProjectUser = _myProjectUser.Find(x => x.ID == id);

            if (myProjectUser == null)
            {
                return HttpNotFound();
            }

            return View(myProjectUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MyProjectUser myProjectUser)
        {
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                var businessLayerResult = _loginOperation.EditUser(myProjectUser);

                if (businessLayerResult.MessageObjList.Count > 0)
                {
                    foreach (var item in businessLayerResult.MessageObjList)
                    {
                        //Bu şekilde bir ERROR eklendiğinde ModelState in value değerlerine bir null değer ekleniyor ama hata olarak da buradaki item veriliyor.Ve bu hata sayfada property hatası olmadığı için ValidationSummary içinde gösteriliyor.
                        ModelState.AddModelError("", item.Message);
                    }
                    return View(myProjectUser);
                }
                else
                {
                    _notificationOKModel.Items.Add(new MessageObj()
                    {
                        MessageCode =
                   MessageCode.RegisterUpdateSuccess,
                        Message = "Başarılı Bir Şekilde Kullanıcı Güncelleştirildi."
                    });
                    _notificationOKModel.Title = "Güncelleme Başarılı.";
                    _notificationErrorViewModel.RedirectingUrl = "/MyProjectUser/Index";
                    return View("OK", _notificationOKModel);
                }
            }
            return View(myProjectUser);
        }

        // GET: MyProjectUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MyProjectUser myProjectUser = _myProjectUser.Find(x => x.ID == id);

            if (myProjectUser == null)
            {
                return HttpNotFound();
            }
            return View(myProjectUser);
        }

        // POST: MyProjectUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyProjectUser myProjectUser = _myProjectUser.Find(x => x.ID == id);

            _myProjectUser.Delete(myProjectUser);

            return RedirectToAction("Index");
        }

    }
}
