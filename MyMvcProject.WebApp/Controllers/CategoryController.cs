using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.WebApp.Filters;
using System.Net;
using System.Web.Mvc;

namespace MyMvcProject.WebApp.Controllers
{
    [Exc]
    [Auth]
    [AuthAdmin]
    public class CategoryController : Controller
    {
        // GET: Home
        IBaseManager<Category> _categoryManager;
        public CategoryController(
            IBaseManager<Category> categorymanager
           )
        {
            _categoryManager = categorymanager;
        }
        public ActionResult Index()
        {
            return View(_categoryManager.Get());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryManager.Find(x => x.ID == id.Value);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                _categoryManager.Insert(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryManager.Find(x => x.ID == id.Value);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                var categoryOne = _categoryManager.Find(x => x.ID == category.ID);
                categoryOne.Title = category.Title;
                categoryOne.Description = category.Description;

                _categoryManager.Update(categoryOne);

                return RedirectToAction("Index");
            }
            return View(category);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryManager.Find(x => x.ID == id.Value);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _categoryManager.Find(x => x.ID == id);
            _categoryManager.Delete(category);
            return RedirectToAction("Index");
        }
    }
}