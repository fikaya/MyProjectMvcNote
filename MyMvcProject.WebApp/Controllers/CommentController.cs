using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.IntermediateTable.Liked;
using MyMvcProject.WebApp.Filters;
using MyMvcProject.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace MyMvcProject.WebApp.Controllers
{
    [Exc]
    public class CommentController : Controller
    {
        IBaseManager<Comment> _commentManager;
        IBaseManager<Note> _noteManager;
        IBaseManager<Liked> _likeManager;

        public CommentController(
            IBaseManager<Comment> commentManager, IBaseManager<Note> noteManager, IBaseManager<Liked> likeManager)
        {
            _commentManager = commentManager;
            _noteManager = noteManager;
            _likeManager = likeManager;
        }
        public ActionResult ShowNoteComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var commentList = _commentManager.GetReference(x => x.NoteID == id, "Note");

            _commentManager.GetReference(x => x.NoteID == id, "MyProjectUser");

            return PartialView("_PartialComments", commentList);
        }
        [Auth]

        public ActionResult Edit(int? id, string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = _commentManager.Find(x => x.ID == id);

            if (comment == null)
            {
                return new HttpNotFoundResult();
            }

            comment.Text = text;
            int resultCount = _commentManager.Update(comment);
            if (resultCount > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        [Auth]

        public ActionResult Delete(int? id, string text)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = _commentManager.Find(x => x.ID == id);

            if (comment == null)
            {
                return new HttpNotFoundResult();
            }

            int resultCount = _commentManager.Delete(comment);

            if (resultCount > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        [Auth]

        public ActionResult Create(Comment comment, int? noteID)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (noteID == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Note note = _noteManager.Find(x => x.ID == noteID);

                if (note == null)
                {
                    return new HttpNotFoundResult();
                }

                comment.NoteID = note.ID;

                comment.MyProjectUserID = CurrentSession.User.ID;

                var resultCount = _commentManager.Insert(comment);

                if (resultCount > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
       
    }
}