using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyMvcProject.BusinessLayer.BusinessLayerResults;
using MyMvcProject.BusinessLayer.ControllersOperation.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Interface.ViewModelInterface;
using MyMvcProject.Entities.Messages;
using MyMvcProject.Entities.Messages.Obj;
using MyMvcProject.Entities.ViewModels;
using MyMvcProject.Entities.ViewModels.Notify;
using MyMvcProject.WebApp.Filters;

namespace MyMvcProject.WebApp.Controllers
{
    [Exc]

    public class HomeController : Controller
    {
        // GET: Home
        IHomeOperation _homeOperation;
        public HomeController(
            IHomeOperation homeOperation
           )
        {
            _homeOperation = homeOperation;
        }

        //Aşağıdaki metotlarımız HomeControllerımız da çeşitli işlemleri görecek metotlarımız.Ve içlerinde çeşitli filtreleme işlemleri yapılıyor. Normalde metotları burada oluşturup bu metotların içerisinde çağırarak filtrelemeleri yapıyordum.Ama yarın bir gün eğer mobil için de uygulama yapılırsa iş kuralları aynı kalmalı düşüncesiyle.Bunları Business katmanı içinde ControlersOperation klsörü içindeki HomeOperation içine yerleştirdim. Ordaki metotlarımın her biri IHomeIndexViewModel dönüyor. Peki bunun anlamı nedir? 
        //Şimdi bu interface içinde aşağıdaki sayfalar da kullancağım modellerim var. Ve bunları bir interface oluşturarak sözleşme imzaladım. HomeIndexViewModel sınıfımda bu işi alıp yapacak. Onun için ben  aşağıdaki sayfalarımın modellerini HomeIndexViewModel olarak yazdım.(Alt-Üst ilişkisi).IHomeIndexViewModel olarak da yazabilirdim. Çünkü o interface den bu özellikler implement ediliyor.

        public ActionResult Index()
        {
            IHomeIndexViewModel viewModel = _homeOperation.CategoryList();
            _homeOperation.NoteList();
            return View(viewModel);
        }
        public ActionResult ByCategory(int? id = null)
        {
            if (id != null)
            {
                IHomeIndexViewModel viewModel = _homeOperation.CategoryList();
                _homeOperation.NoteList(id);
                //Direkt Index viewını açıp modeli o sayfaya yerleştirecek.
                return View("Index", viewModel);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult MostLiked()
        {
            IHomeIndexViewModel viewModel = _homeOperation.CategoryList();
            _homeOperation.NoteOrderByDescending();
            //Direkt Index viewını açıp modeli o sayfaya yerleştirecek.
            return View("Index", viewModel);
        }

        public ActionResult About()
        {
            IHomeIndexViewModel viewModel = _homeOperation.CategoryList();
            return View(viewModel);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult HasError()
        {
            return View();
        }
    }
}