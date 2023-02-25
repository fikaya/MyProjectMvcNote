using MyEvernote.CommonLayer.Helpers;
using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.BusinessLayer.BusinessLayerResults;
using MyMvcProject.BusinessLayer.Concrete;
using MyMvcProject.BusinessLayer.ControllersOperation.Abstract;
using MyMvcProject.CommonLayer.Helpers;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Interface.ViewModelInterface;
using MyMvcProject.Entities.Messages.Enums;
using MyMvcProject.Entities.Messages.Obj;
using MyMvcProject.Entities.ViewModels;
using System;

namespace MyMvcProject.BusinessLayer.ControllersOperation
{
    public class LoginOperation : ILoginOperation
    {
        private IBaseManager<MyProjectUser> _myProjectUserManager;
        private IBusinessBaseLayerResult<MyProjectUser> _businessLayerResult;
        public LoginOperation
        (
            IBaseManager<MyProjectUser> myProjectManager,
            IBusinessBaseLayerResult<MyProjectUser> businessLayerMessageObjResult
        )
        {
            _myProjectUserManager = myProjectManager;
            _businessLayerResult = businessLayerMessageObjResult;
        }
        public IBusinessBaseLayerResult<MyProjectUser> RegisterUser(RegisterViewModel registerViewModel)
        {
            //Kullanıcı username kontrolü
            //Kullanıcı E-posta kontrolü
            //Kayıt İşlemi
            //Aktivasyon E-Postasını gönderimi
            var myProjectUserInsert = new MyProjectUser()
            {
                UserName = registerViewModel.UserName,
                EMail = registerViewModel.UserMail,
                Password = registerViewModel.Password,
                IsActive = false,
                IsAdmin = false
            };

            var myProjectUser = _myProjectUserManager.Find(x => x.UserName == myProjectUserInsert.UserName || x.EMail == myProjectUserInsert.EMail);

            return Register(myProjectUserInsert, myProjectUser);
        }
        public IBusinessBaseLayerResult<MyProjectUser> RegisterUser(MyProjectUser myProjectUserInsert)
        {
            //Kullanıcı username kontrolü
            //Kullanıcı E-posta kontrolü
            //Kayıt İşlemi
            //Aktivasyon E-Postasını gönderimi
            var myProjectUser = _myProjectUserManager.Find(x => x.UserName == myProjectUserInsert.UserName || x.EMail == myProjectUserInsert.EMail);

            return Register(myProjectUserInsert,myProjectUser);
        }

        //Register User metotlarında ortak olan alanları ben bir metot içinde toplamış oldum.
        private IBusinessBaseLayerResult<MyProjectUser> Register(MyProjectUser myProjectUser, MyProjectUser myProjectUserInsert)
        {
            if (myProjectUserInsert != null)
            {
                //Eğer null değilse ya username konusunda ya da e-mail konusunda bir problem var demektir.
                if (myProjectUser.UserName == myProjectUserInsert.UserName)
                {
                    _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.UserNameAlreadyExists, Message = "Kayıt Olmak İstediğiniz Kullanıcı Adı Kayıtlı." });
                }
                if (myProjectUser.EMail == myProjectUserInsert.EMail)
                {
                    _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.EMailAlreadyExists, Message = "Kayıt Olmak İstediğiniz E-Posta Adresi Kayıtlı." });
                }
            }
            else
            {
                int dbResult = _myProjectUserManager.Insert(myProjectUser);
                if (dbResult > 0)
                {
                    _businessLayerResult.Result = myProjectUser;

                    if (!_businessLayerResult.Result.IsActive)
                    {
                        //TODO:aktivasyon maili atılacak
                        var rootUrl = ConfigHelper.Get<string>("SiteRootUrl");
                        var activateUrl = $"{rootUrl}/Login/UserActivation/{myProjectUser.ActiveCode}";
                        var body = ($"Merhaba {myProjectUser.UserName};<br><br> Hesabınızı aktifleştirmek için <a href='{activateUrl}' target='_blank'>tıklayınız.</a>");
                        MailHelper.SendMail(body, myProjectUser.EMail, "Hesap Aktifleştirme");
                    }
                }
                else
                {
                    _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.UserCouldNotInserted, Message = "Kayıt Eklenemedi." });
                }
            }
            return _businessLayerResult;
        }

        public IBusinessBaseLayerResult<MyProjectUser> LoginUser(LoginViewModel loginViewModel)
        {
            //Giriş Kontrolü
            //Hesap aktive edilmiş mi?

            //Yönlendirme
            //Session da kullanıcı bilgi saklama 

            //Şimdi bizim UI katmanı Windows uygulaması,Web Uygulaması vs. olabilir. Ve UL katmanındaki işlemlerimde ben istikrarı ve düzeni sağlamak adına business layer dll nini alıp kullanacğım. Diyelimki bu metodu kullanacağım. Ve bir windows UI katmanı üzeirnde bu metodu çağırdığımızı hayal edelim. Windows uygulamasının Session ile ne alakası olur değil mi?. Session da işlem yapmak mecburiyetinde kalcaz böyle yaparsak. Ama ben bu session da kullanıcı bilgi saklama işini UI katmanı üzerinde yaparsam sıkntı olmaz.
            //Sonuç olarak Business katmanında yazılan kodlar her zaman her UI katmanında çalışacak kodlar olmalı.
            _businessLayerResult.Result = _myProjectUserManager.Find(x => x.EMail == loginViewModel.UserMail && x.Password == loginViewModel.Password);

            if (_businessLayerResult.Result != null)
            {
                if (!_businessLayerResult.Result.IsActive)
                {
                    _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.UserIsNotActive, Message = "Kullanıcı Aktifleştirilmemiştir." });
                    _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.CheckYourEMail, Message = "Lütfen E-Posta Adresinizi Kontrol Ediniz." });
                }
            }
            else
            {
                _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.UsernameOrPasswordWrong, Message = "Kullanıcı Adı Ya Da Şifre Uyuşmuyor." });
            }

            return _businessLayerResult;
        }
        public IBusinessBaseLayerResult<MyProjectUser> ActivateUser(Guid ID)
        {
            _businessLayerResult.Result = _myProjectUserManager.Find(x => x.ActiveCode == ID);
            if (_businessLayerResult.Result != null)
            {
                if (_businessLayerResult.Result.IsActive)
                {
                    _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.UserAlreadyActive, Message = "Kullanıcı Zaten Aktif Edilmiştir" });
                    return _businessLayerResult;
                }
                else
                {
                    _businessLayerResult.Result.IsActive = true;
                    _myProjectUserManager.Update(_businessLayerResult.Result);
                    return _businessLayerResult;
                }
            }
            else
            {
                //Biri sistemi denemeye çalıştığında custom bir id değeri de gönderebilir. O şekilde herhangi bir Guid değeri nesneyi bulammayacak.
                _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.ActivateIDDoesNotExists, Message = "Aktifleştirilecek Kullanıcı Bulunamadı." });
                return _businessLayerResult;
            }
        }
        public IBusinessBaseLayerResult<MyProjectUser> GetUserByID(int? id)
        {
            _businessLayerResult.Result = _myProjectUserManager.Find(x => x.ID == id);
            if (_businessLayerResult.Result == null)
            {
                _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.UserNotFound, Message = "Kullanıcı Bulunamadı." });
            }
            return _businessLayerResult;
        }
        public IBusinessBaseLayerResult<MyProjectUser> EditUser(MyProjectUser myProjectUser)
        {
            var db_user = _myProjectUserManager.Find(x => x.UserName == myProjectUser.UserName || x.EMail == myProjectUser.EMail);

            if (db_user != null && db_user.ID != myProjectUser.ID)
            {
                if (db_user.UserName == myProjectUser.UserName)
                {
                    _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.UserNameAlreadyExists, Message = "Kullanıcı Adı Kayıtlı." });
                }
                if (db_user.EMail == myProjectUser.EMail)
                {
                    _businessLayerResult.MessageObjList.Add(
                        new MessageObj()
                        {
                            MessageCode = MessageCode.UserNameAlreadyExists,
                            Message = "E-Posta Adresi Kayıtlı."
                        });
                }
                return _businessLayerResult;
            }

            _businessLayerResult.Result = myProjectUser;

            if (string.IsNullOrEmpty(myProjectUser.ProfileImageFileName) == false)
            {
                //Bu koşulu yapma sebebimiz kullanıcı resmini güncelliyormu.
                _businessLayerResult.Result.ProfileImageFileName = myProjectUser.ProfileImageFileName;
            }

            if (_myProjectUserManager.Update(_businessLayerResult.Result) == 0)
            {
                _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.ProfileCouldNotUpdated, Message = "Profil Güncellenemedi." });
            }

            return _businessLayerResult;
        }
        public IBusinessBaseLayerResult<MyProjectUser> RemoveUserByID(int id)
        {
            var user = _myProjectUserManager.Find(x => x.ID == id);
            if (user != null)
            {
                if (_myProjectUserManager.Delete(user) == 0)
                {
                    _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.UserCouldNotRemove, Message = "Kullanıcı Silinemedi" });
                    return _businessLayerResult;
                }
            }
            else
            {
                _businessLayerResult.MessageObjList.Add(new MessageObj() { MessageCode = MessageCode.UserCouldNotFind, Message = "Kullanıcı Bulunamadı" });
            }
            return _businessLayerResult;
        }

    }
}
