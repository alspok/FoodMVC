using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodMVC.Models;
using FoodMVC.Helpers;

namespace FoodMVC.Controllers
{
    public class FoodsController : Controller
    {
        private readonly FoodDatabaseEntities6 db = new FoodDatabaseEntities6();
        private readonly FoodDatabaseEntities10 ldb = new FoodDatabaseEntities10();

        // GET: Foods
        public ActionResult Index()
        {
            TempData["DateTime"] = DateTime.Now.DayOfWeek;

            return View(db.Foods.ToList());
        }

        // GET: Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }



            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Food food)
        {
            food.Type = food.Selection == "maistas" ? 0 : 1;

            if (ModelState.IsValid)
            {
                db.Foods.Add(food);
                db.SaveChanges();
                db.Dispose();

                var userId = Convert.ToInt32(Session["UserId"]);
                ModTime modTime = new ModTime();
                modTime.ModTimeChange(userId);

                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Food food)
        {
            food.Type = food.Selection == "maistas" ? 0 : 1;

            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();

                var userId = Convert.ToInt32(Session["UserId"]);
                ModTime modTime = new ModTime();
                modTime.ModTimeChange(userId);

                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                var userId = Convert.ToInt32(Session["UserId"]);
                ModTime modTime = new ModTime();
                modTime.ModTimeChange(userId);

                return HttpNotFound();
            }

            return View(food);
        }

        //GET:Foods/SoftDelete/5
        public ActionResult SoftDelete(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var softDel = db.Foods.Find(id);

            switch (softDel.Softdel)
            {
                case 0:
                    softDel.Softdel = 1;
                    break;
                case 1:
                    softDel.Softdel = 2;
                    break;
                case 2:
                    softDel.Softdel = 0;
                    break;
            }

            db.SaveChanges();
            db.Dispose();

            var userId = Convert.ToInt32(Session["UserId"]);
            ModTime modTime = new ModTime();
            modTime.ModTimeChange(userId);

            TempData["Role"] = ldb.LogRegs.Select(v => v.Role); ;

            return RedirectToAction("Index");
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
            db.SaveChanges();
            db.Dispose();

            var userId = Convert.ToInt32(Session["UserId"]);
            ModTime modTime = new ModTime();
            modTime.ModTimeChange(userId);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
