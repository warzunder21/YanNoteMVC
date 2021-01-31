using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

using YanNote.Models;
using YanNote.Models.ViewModels;

namespace NoteMVC.Controllers
{
    public class NoteController : Controller
    {

        private readonly YanNoteContext _db;
        
        
        public NoteController(YanNoteContext db)
        {
            
            _db = db;
        }
        
        public IActionResult Index()
        {        
            IEnumerable<Note> objList = _db.Note.Include(r => r.Rem);
            ViewBag.NoteTags = _db.NoteTag.Include(t => t.Tags);
            return View(objList);
        }



        //GET - CREATE
        public IActionResult Create()
        {
            NoteVM noteVM = new NoteVM()
            {
                Note = new Note(),
                TagSelectList = new List<SelectListItem>(),
                NoteTags = null

            };
            foreach (Tag t in _db.Tag)
            {
                noteVM.TagSelectList.Add(
                    new SelectListItem
                    {
                        Text = t.Name,
                        Value = t.Id.ToString()
                    });
            }
            return View(noteVM);

        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoteVM noteVM)
        {
            if (ModelState.IsValid)
            {
                Note newNote = noteVM.Note;

                if (noteVM.NoteTags != null)
                {
                    foreach (var t in _db.Tag)
                    {
                        if (noteVM.NoteTags.Contains(t.Id)) {
                            NoteTag noteTag = new NoteTag { NotesId = newNote.Id, TagsId = t.Id };
                            newNote.NoteTags.Add(noteTag); }
                    }
                }
                _db.Note.Add(newNote);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noteVM);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            NoteVM noteVM = new NoteVM()
            {
                Note = _db.Note.Find(id),
                TagSelectList = new List<SelectListItem>(),
                NoteTags = _db.NoteTag.Where(i => i.NotesId == id).Select(sc => sc.TagsId)

            };
            foreach (Tag t in _db.Tag)
            {
                noteVM.TagSelectList.Add(
                    new SelectListItem
                    {
                        Text = t.Name,
                        Value = t.Id.ToString()
                    });
            }

            if (noteVM.Note == null)
            {
                return NotFound();
            }


            return View(noteVM);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NoteVM noteVM)
        {
            if (noteVM.Note == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                Note newNote = noteVM.Note;
                _db.Note.Remove(_db.Note.Find(newNote.Id));
                _db.RemoveRange(_db.NoteTag.Where(i => i.NotesId == newNote.Id));
                if (noteVM.NoteTags != null)
                {
                    foreach (var t in _db.Tag)
                    {
                        if (noteVM.NoteTags.Contains(t.Id))
                        {
                            NoteTag noteTag = new NoteTag { NotesId = newNote.Id, TagsId = t.Id };
                            newNote.NoteTags.Add(noteTag);
                        }
                    }
                }
                _db.Add(newNote);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noteVM);
            

        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Note.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Note.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Note.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
