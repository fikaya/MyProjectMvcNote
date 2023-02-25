using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Interface.NavigationInterface;
using MyMvcProject.Entities.IntermediateTable.Liked;
using MyMvcProject.WebApp.Filters;
using MyMvcProject.WebApp.Models;

namespace MyMvcProject.WebApp.Controllers
{
    [Exc]
    public class NoteController : Controller
    {
        IBaseManager<Note> _noteManager;
        IBaseManager<Category> _categoryManager;
        IBaseManager<Liked> _likeManager;

        public NoteController(IBaseManager<Note> noteManager, IBaseManager<Category> categoryManager, IBaseManager<Liked> likeManager)
        {
            _noteManager = noteManager;
            _categoryManager = categoryManager;
            _likeManager = likeManager;
        }

        [Auth]
        public ActionResult Index()
        {
            var list = _noteManager.GetReference(x => x.MyProjectUserID == CurrentSession.User.ID, "Category", "MyProjectUser").OrderByDescending(x => x.ModifiedOn);
            return View(list);
        }
        [Auth]
        public ActionResult MyLikedNotes()
        {
            var list = _likeManager.GetReference(x => x.MyProjectUserID == CurrentSession.User.ID, "Note", "MyProjectUser");

            var listNote = new List<Note>();

            foreach (var item in list)
            {
                var note = _noteManager.GetReference(x => x.ID == item.NoteID, "Category").FirstOrDefault();
                listNote.Add(note);
            }

            return View(listNote);
        }

        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var note = _noteManager.GetReference(x => x.MyProjectUserID == CurrentSession.User.ID && x.ID == id, "Category", "MyProjectUser").FirstOrDefault();

            if (note == null)
            {
                return HttpNotFound();
            }

            return View(note);
        }

        [Auth]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryManager.Get(), "ID", "Title");
            return View();
        }

        [HttpPost]
        [Auth]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                note.MyProjectUserID = CurrentSession.User.ID;
                _noteManager.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_categoryManager.Get(), "ID", "Title", note.CategoryID);
            return View(note);
        }
        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var note = _noteManager.GetReference(x => x.MyProjectUserID == CurrentSession.User.ID && x.ID == id, "Category", "MyProjectUser").FirstOrDefault();

            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_categoryManager.Get(), "ID", "Title", note.CategoryID);

            return View(note);
        }

        [HttpPost]
        [Auth]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                var noteOne = _noteManager.Find(x => x.ID == note.ID);
                noteOne.IsDraft = note.IsDraft;
                noteOne.CategoryID = note.CategoryID;
                noteOne.Text = note.Text;
                noteOne.Title = note.Title;

                _noteManager.Update(noteOne);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_categoryManager.Get(), "ID", "Title", note.CategoryID);
            return View(note);
        }
        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var note = _noteManager.GetReference(x => x.MyProjectUserID == CurrentSession.User.ID && x.ID == id, "Category", "MyProjectUser").FirstOrDefault();

            if (note == null)
            {
                return HttpNotFound();
            }

            return View(note);
        }

        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var note = _noteManager.Find(x => x.ID == id);
            _noteManager.Delete(note);
            return RedirectToAction("Index");
        }
        [Auth]

        public ActionResult GetLiked(params int[] ids)
        {
            //Kullanıcının like yaptığı notları dönmemiz gerek.
            if (CurrentSession.User != null)
            {
                var likedList = _likeManager.GetReference(x => x.MyProjectUserID == CurrentSession.User.ID && ids.Contains(x.NoteID.Value), "MyProjectUser", "Note");

                List<int?> likedNoteIds = new List<int?>();

                for (int i = 0; i < likedList.Count; i++)
                {
                    likedNoteIds.Add(likedList[i].NoteID);
                }

                return Json(new { result = likedNoteIds });
            }

            return Json(new { result = "" });

        }
        [Auth]

        public ActionResult SetLikeState(int noteID, bool liked)
        {
            var note = _noteManager.Find(x => x.ID == noteID);

            if (CurrentSession.User != null)
            {
                var like = _likeManager.Find(x => x.NoteID == noteID && x.MyProjectUserID == CurrentSession.User.ID);

                int res = 0;

                if (like != null && liked == false)
                {
                    //Karşı taraftan değer gelirken eğer like lı ise false şeklinde gelecek çünkü ben beğeniyi kaldıracağım. Burada sistemde var ve false ise değeri silmem gerek.
                    res = _likeManager.Delete(like);
                }
                else if (like == null && liked == true)
                {
                    //Yeni bir like işlemi yapıldığında o like yoktur ve liked değeri true olarak gelmiştir.
                    res = _likeManager.Insert(new Liked() { MyProjectUserID = CurrentSession.User.ID, NoteID = noteID });
                }
                if (res > 0)
                {
                    if (liked)
                    {
                        note.LikeCount++;
                    }
                    else
                    {
                        note.LikeCount--;
                    }
                    res = _noteManager.Update(note);
                    return Json(new { hasError = false, errorMessage = string.Empty, result = note.LikeCount });
                }
            }
            return Json(new { hasError = true, errorMessage = "Beğenme İşlemi Gerçekleştirilemedi.", result = note.LikeCount });
        }
        [Auth]
        public ActionResult GetNoteText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var note = _noteManager.GetReference(x => x.ID == id, "Category", "MyProjectUser").FirstOrDefault();

            if (note == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialNoteText", note);
        }
    }
}
