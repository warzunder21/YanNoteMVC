using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YanNote.Models;

namespace NoteMVC.Controllers
{
    public class TagController : Controller
    {
        private readonly YanNoteContext _db;

        public TagController(YanNoteContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Tag> objList = _db.Tag;
            return View(objList);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tag.Add(obj);
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
            var obj = _db.Tag.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tag.Update(obj);
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
            var obj = _db.Tag.Find(id);
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
            var obj = _db.Tag.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Tag.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }


    }
}
