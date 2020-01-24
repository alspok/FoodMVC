using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FoodMVC.Models;

namespace FoodMVC.Controllers
{
    public class MailController : Controller
    {
        private readonly FoodDatabaseEntities6 db = new FoodDatabaseEntities6();
        private readonly FoodDatabaseEntities10 ldb = new FoodDatabaseEntities10();

        // GET: Mail
        public ActionResult Index()
        {
            return View(ldb.LogRegs.ToList());
        }

        public ActionResult EmailList(SendMailModel sendMailModel)
        {
            sendMailModel.EmailToList = ldb.LogRegs.Select(u => u.Email).ToList();

            return View(sendMailModel);
        }

        public ActionResult SendMail(SendMailModel sendMailModel)
        {
            string foodAreaContent = "Maistas\n\n";
            string nonfoodAreaContent = "\nKitos prekės\n\n";

            List<Food> foodList = db.Foods.ToList();

            int i = 1;
            foreach (var item in foodList)
            {
                if (item.Type == 0 && item.Softdel == 0)
                {
                    foodAreaContent += i++ + ". " + item.Item + " " + item.Quantity + " " + item.Notes + "\n";
                }
            }

            i = 1;
            foreach (var item in foodList)
            {
                if (item.Type == 1 && item.Softdel == 0)
                {
                    nonfoodAreaContent += i++ + ". " + item.Item + " " + item.Quantity + " " + item.Notes + "\n";
                }

            }

            TempData["FoodAreaContent"] = foodAreaContent + nonfoodAreaContent;

            return View(sendMailModel);
        }

        [HttpPost]
        public ActionResult Email(SendMailModel sendMailModel)
        {
            sendMailModel.EmailText = Request.Form["foodarea"];

            object role = Session["UserRole"];
            ViewBag.EmailTo = sendMailModel.EmailTo;



            if (role.Equals("guest"))
            {
                sendMailModel.EmailTo = Session["UserEmail"].ToString();
            }

            using (var mm = new MailMessage(sendMailModel.EmailFrom, sendMailModel.EmailTo))
            {
                mm.Subject = sendMailModel.Subject;
                mm.Body = sendMailModel.EmailText;
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "in-v3.mailjet.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(sendMailModel.EmailUser, sendMailModel.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 25;
                    smtp.Send(mm);
                    ViewBag.Message = DateTime.Now;
                }
            }

            return View();
        }

        // GET: Mail/Details/5
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

        // GET: Mail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,Email,Role")] LogReg logReg)
        {
            if (ModelState.IsValid)
            {
                ldb.LogRegs.Add(logReg);
                ldb.SaveChanges();
                ldb.Dispose();

                return RedirectToAction("Index");
            }

            return View(logReg);
        }

        // GET: Mail/Edit/5
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

        // POST: Mail/Edit/5
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
                db.Dispose();

                return RedirectToAction("Index");
            }
            return View(logReg);
        }

        // GET: Mail/Delete/5
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

        // POST: Mail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogReg logReg = ldb.LogRegs.Find(id);
            ldb.LogRegs.Remove(logReg);
            ldb.SaveChanges();
            ldb.Dispose();

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
