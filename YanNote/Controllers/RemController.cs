using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YanNote.Data;
using YanNote.Models;

namespace NoteMVC.Controllers
{
    public class RemController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RemController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Rem> objList = _db.Rem.Include(u => u.Note);
            return View(objList);
        }

        //GET - CREATE
        public IActionResult Create(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rem obj)
        {
            if (ModelState.IsValid)
            {
                Rem newRem = new Rem { EventDate = obj.EventDate, NoteId = obj.NoteId};
                _db.Rem.Add(newRem);
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
            var obj = _db.Rem.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Rem obj)
        {
            if (ModelState.IsValid)
            {
                _db.Rem.Update(obj);
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
            var obj = _db.Rem.Find(id);
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
            var obj = _db.Rem.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Rem.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");


        }


    }
}
