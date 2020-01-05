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
    public class LogsRegsController : Controller
    {
        private readonly FoodDatabaseEntities6 db = new FoodDatabaseEntities6();
        private readonly FoodDatabaseEntities10 ldb = new FoodDatabaseEntities10();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogReg logReg)
        {
            if (ldb.LogRegs.Where(u => u.UserName == logReg.UserName).FirstOrDefault() != null &&
                ldb.LogRegs.Where(u => u.Email == logReg.Email).FirstOrDefault() != null &&
                ldb.LogRegs.Where(u => u.Password == logReg.Password).FirstOrDefault() != null)
            {
                int userId = ldb.LogRegs.Where(u => u.Email == logReg.Email).Select(v => v.Id).FirstOrDefault();
                string userEmail = ldb.LogRegs.Where(u => u.Email == logReg.Email).Select(v => v.Email).FirstOrDefault();
                string userRole = ldb.LogRegs.Where(u => u.Email == logReg.Email).Select(v => v.Role).FirstOrDefault();

                LogModTime logModTime = new LogModTime();
                logModTime.LogTimeChange(userId);

                Session["UserId"] = userId;
                Session["UserEmail"] = userEmail;
                Session["UserRole"] = userRole;

                TempData["User"] = ldb.LogRegs.Where(u => u.Id == userId).Select(v => v.UserName).FirstOrDefault();
                TempData["Email"] = ldb.LogRegs.Where(u => u.Id == userId).Select(v => v.Email).FirstOrDefault();
                TempData["Role"] = ldb.LogRegs.Where(u => u.Id == userId).Select(v => v.Role).FirstOrDefault();

                return RedirectToAction("Index", "Foods");
            }

            ViewBag.NoLogin = "Neteisingas vartotojo vardas, slaptažodis arba el. pašto adresas";
            return View();
        }

        // GET: Regs
        public ActionResult Index()
        {
            return View(ldb.LogRegs.ToList());
        }

        // GET: Regs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LogReg logReg = ldb.LogRegs.Find(id);

            if (logReg == null)
            {
                return HttpNotFound();
            }
            return View(logReg);
        }

        // GET: Regs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Regs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,UserName,Password,Email,Role")] LogReg logReg)
        public ActionResult Create(LogReg logReg)
        {
            if (ModelState.IsValid &&
                ldb.LogRegs.Where(u => u.UserName == logReg.UserName).FirstOrDefault() == null &&
                ldb.LogRegs.Where(u => u.Email == logReg.Email).FirstOrDefault() == null)
            {
                ldb.LogRegs.Add(logReg);
                ldb.SaveChanges();

                LogReg lastReg = ldb.LogRegs.Find(logReg.Id);

                return View("Register", lastReg);
            }
            else
            {
                ViewBag.Message1 = "Vartotojo vardas arba el. pašto adresas jau registruoti.";
                ViewBag.Message2 = "Registruokitės dar kartą!";

                return View("Create");
            }
        }

        

        // GET: Regs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LogReg logReg = ldb.LogRegs.Find(id);
            if (logReg == null)
            {
                return HttpNotFound();
            }
            return View(logReg);
        }

        // POST: Regs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,Email,Role")] LogReg logReg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logReg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("~/Regs/Register");
            }
            return View(logReg);
        }

        // GET: Regs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogReg logReg = ldb.LogRegs.Find(id);
            if (logReg == null)
            {
                return HttpNotFound();
            }
            return View(logReg);
        }

        // POST: Regs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogReg logReg = ldb.LogRegs.Find(id);
            ldb.LogRegs.Remove(logReg);
            ldb.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
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
