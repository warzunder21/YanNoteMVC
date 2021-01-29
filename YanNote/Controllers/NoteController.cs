using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using YanNote.Data;
using YanNote.Models;

namespace NoteMVC.Controllers
{
    public class NoteController : Controller
    {

        private readonly ApplicationDbContext _db;
        
        
        public NoteController(ApplicationDbContext db)
        {
            
            _db = db;
        }
        
        public IActionResult Index()
        {        
            IEnumerable<Note> objList = _db.Note.Include(t => t.Tags).Include(r => r.Rem);
            return View(objList);
        }

        

        //GET - CREATE
        public IActionResult Create()
        {
            ViewBag.Tags = _db.Tag.ToList();
            
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Note obj, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                Note newNote = new Note {Title = obj.Title, Text = obj.Text};
                _db.Note.Add(newNote);                
                if (selectedTags != null)
                {
                    foreach (var t in _db.Tag.Where(u => selectedTags.Contains(u.Id))) {
                        newNote.Tags.Add(t);
                    } 
                }                
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
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
            
            ViewBag.Tags = _db.Tag.ToList();
            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Note obj, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                               
                Note newNote = _db.Note.Find(obj.Id);                
                newNote.Title = obj.Title;
                newNote.Text = obj.Text;
                newNote.Tags.Clear();               
                if (selectedTags != null)
                {
                    foreach (var t in _db.Tag.Where(u => selectedTags.Contains(u.Id)))
                    {
                        newNote.Tags.Add(t);
                    }
                }               
                _db.Note.Update(newNote);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

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
