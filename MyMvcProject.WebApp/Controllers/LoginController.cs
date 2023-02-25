using MyMvcProject.BusinessLayer.ControllersOperation;
using MyMvcProject.BusinessLayer.ControllersOperation.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Messages;
using MyMvcProject.Entities.Messages.Enums;
using MyMvcProject.Entities.Messages.Obj;
using MyMvcProject.Entities.ViewModels;
using MyMvcProject.Entities.ViewModels.Notify;
using MyMvcProject.WebApp.Filters;
using MyMvcProject.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyMvcProject.WebApp.Controllers
{
    [Exc]

    public class LoginController : Controller
    {
        ILoginOperation _loginOperation;
        INotifyViewModel<MessageObj, ErrorViewModel> _notificationErrorViewModel;
        INotifyViewModel<MessageObj, InformationViewModel> _notificationInfoViewModel;
        INotifyViewModel<MessageObj, OKViewModel> _notificationOKModel;

        public LoginController(
            ILoginOperation loginOperation,
            INotifyViewModel<MessageObj, ErrorViewModel> notificationErrorViewModel,
            INotifyViewModel<MessageObj, OKViewModel> notificationOKModel,
            INotifyViewModel<MessageObj, InformationViewModel> notificationInfoViewModel
            )
        {
            _loginOperation = loginOperation;
            _notificationErrorViewModel = notificationErrorViewModel;
            _notificationOKModel = notificationOKModel;
            _notificationInfoViewModel = notificationInfoViewModel;
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel item1)
        {
            if (ModelState.IsValid)
            {
                //Eğer bu şartın içine girilmiş ise modelimizde herhangi bir hata yoktur.
                //Şimdi ben bazı kontroller yaparak custom hatalar da ekleyebilirim.

                var businessLayerResult = _loginOperation.RegisterUser(item1);

                if (businessLayerResult.MessageObjList.Count > 0)
                {
                    foreach (var item in businessLayerResult.MessageObjList)
                    {
                        //Bu şekilde bir ERROR eklendiğinde ModelState in value değerlerine bir null değer ekleniyor ama hata olarak da buradaki item veriliyor.Ve bu hata sayfada property hatası olmadığı için ValidationSummary içinde gösteriliyor.
                        ModelState.AddModelError("", item.Message);
                    }
                    return View("Login", Tuple.Create<RegisterViewModel, LoginViewModel>(item1, null));
                }
                else
                {
                    _notificationOKModel.Items.Add(new MessageObj()
                    {
                        MessageCode =
                        MessageCode.RegisterSuccess,
                        Message = "Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden not ekleyemez ve beğeni yapamazsınız."
                    });
                    _notificationOKModel.Title = "Kayıt Başarılı";
                    return View("OK", _notificationOKModel);
                }
            }
            else
            {
                return View("Login", Tuple.Create<RegisterViewModel, LoginViewModel>(item1, null));
            }
        }
        public ActionResult UserActivation(Guid ID)
        {
            //Kullanıcı aktivasyonu sağlanacak.
            //Aktivasyon sağlanıdktan sonra başarılı oldu ise bir sayfaya yönlendirmek gerekecek.
            var businessLayerResult = _loginOperation.ActivateUser(ID);

            if (businessLayerResult.MessageObjList.Count > 0)
            {
                _notificationErrorViewModel.Title = "Geçersiz İşlem";
                for (int i = 0; i < businessLayerResult.MessageObjList.Count; i++)
                {
                    _notificationErrorViewModel.Items.Add(businessLayerResult.MessageObjList[i]);
                }
                return View("Error", _notificationErrorViewModel);
            }
            else
            {
                _notificationInfoViewModel.Title = "Hesap Aktifleştirildi.";
                _notificationInfoViewModel.RedirectingUrl = "/Home/Index";
                _notificationInfoViewModel.Items.Add(new MessageObj() { MessageCode = MessageCode.ActivationSuccess, Message = "Hesabınız aktifleştirildi.Artık not paylaşabilir ve beğenme yapabilirsiniz." });
                return View("Info", _notificationInfoViewModel);
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel item2)
        {
            if (ModelState.IsValid)
            {
                var businessLayerResult = _loginOperation.LoginUser(item2);

                if (businessLayerResult.MessageObjList.Count > 0)
                {
                    foreach (var item in businessLayerResult.MessageObjList)
                    {
                        ModelState.AddModelError("", item.Message);
                        //Peki ben dönen hataların türüüne göre hareket etmek istersem nasıl hareket edeceğim?
                        //Sayfaya yönlenmeden önce bir şeyler yapmak istiyorum. 
                        //Örneğin Kullanıcı Adı Ve Şifre Eşleşmiyor hatası ise şunu yap.
                        //Örneğin Kullanıcı Aktif Değil hatası ise  adamı başka bir sayfaya yönlendir. 
                        //Ben bunu nasıl anlayabilirim .İşte tam olarak onu çözeceğiz.
                    }
                    return View("Login", Tuple.Create<RegisterViewModel, LoginViewModel>(null, item2));
                }
                else
                {
                    CurrentSession.Set("User", businessLayerResult.Result);
                    return Redirect("/Home/Index");
                }
            }
            else
            {
                return View("Login", Tuple.Create<RegisterViewModel, LoginViewModel>(null, item2));
            }
        }
        public ActionResult LogOut()
        {
            CurrentSession.Clear();
            return Redirect("/Home/Index");
        }
        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var businessLayerResult = _loginOperation.GetUserByID(id);

            if (businessLayerResult == null)
            {
                return HttpNotFound();
            }

            return View(businessLayerResult);
        }
        [Auth]
        public ActionResult ShowProfile()
        {
            return ShowAndEditProfile();
        }
        [Auth]
        public ActionResult EditProfile()
        {
            return ShowAndEditProfile();
        }

        [Auth]
        [HttpPost]
        public ActionResult EditProfile(MyProjectUser myProjectUser, HttpPostedFileBase profileImage)
        {
            //Burada bir durum var. ModifiedUserName alanı da zorunlu bir alan olduğu için onun da doldurulması gerek olarak anlıyor ve false dönüyor aşağıdaki koşul için zaten ben gidip update metdou içinde onu doldrucam.Onun için State içinden bunu çıkarmam gerekecek.
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                if (
                 profileImage != null &&
                 (
                 profileImage.ContentType == "image/jpeg"
                 ||
                 profileImage.ContentType == "image/jpg"
                 ||
                 profileImage.ContentType == "image/png"
                 )
                 )
                {
                    string fileName = $"user_{myProjectUser.ID}.{profileImage.ContentType.Split('/')[1]}";
                    profileImage.SaveAs(Server.MapPath($"~/Images/{fileName}"));
                    myProjectUser.ProfileImageFileName = fileName;
                }

                var businessLayerResult = _loginOperation.EditUser(myProjectUser);

                if (businessLayerResult.MessageObjList.Count > 0)
                {
                    for (int i = 0; i < businessLayerResult.MessageObjList.Count; i++)
                    {
                        _notificationErrorViewModel.Items.Add(businessLayerResult.MessageObjList[i]);
                    }
                    _notificationErrorViewModel.Title = "Profil Güncellenemedi.";
                    _notificationErrorViewModel.RedirectingUrl = "/Login/EditProfile";
                    return View("Error", _notificationErrorViewModel);
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
                    _notificationErrorViewModel.RedirectingUrl = "/Login/ShowProfile";
                    CurrentSession.Set("User", businessLayerResult.Result);                    return View("OK", _notificationOKModel);
                }
            }
            return View(myProjectUser);
        }
        [Auth]

        public ActionResult RemoveProfile()
        {
            var businessLayerResult = _loginOperation.RemoveUserByID(CurrentSession.User.ID);

            if (businessLayerResult.MessageObjList.Count > 0)
            {
                for (int i = 0; i < businessLayerResult.MessageObjList.Count; i++)
                {
                    _notificationErrorViewModel.Items.Add(businessLayerResult.MessageObjList[i]);
                }
                _notificationErrorViewModel.Title = "Profil Silinemedi.";
                _notificationErrorViewModel.RedirectingUrl = "/Home/ShowProfile";

                return View("Error", _notificationErrorViewModel);
            }
            CurrentSession.Clear();
            return Redirect("/Home//Index");
        }
        [Auth]
        private ActionResult ShowAndEditProfile()
        {

            var businessLayerResult = _loginOperation.GetUserByID(CurrentSession.User.ID);
            if (businessLayerResult.MessageObjList.Count > 0)
            {
                //Kullanıcıyı bir hata ekranına yönlendirmek gerekiyor.
                _notificationErrorViewModel.Title = "Geçersiz İşlem";
                for (int i = 0; i < businessLayerResult.MessageObjList.Count; i++)
                {
                    _notificationErrorViewModel.Items.Add(businessLayerResult.MessageObjList[i]);
                }
                return View("Error", _notificationErrorViewModel);
            }
            return View(businessLayerResult.Result);
        }
    }
}