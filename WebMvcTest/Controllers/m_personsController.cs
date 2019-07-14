using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMvcTest.Models;

namespace WebMvcTest.Controllers
{
    public class m_personsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: m_persons
        public ActionResult Index()
        {
            return View(db.m_persons.ToList());
        }

        // GET: m_persons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_persons m_persons = db.m_persons.Find(id);
            if (m_persons == null)
            {
                return HttpNotFound();
            }
            return View(m_persons);
        }

        // GET: m_persons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: m_persons/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,personId,personName,mailAddress,phoneNumber,detail,createdDatetime,updatedDatetime")] m_persons m_persons)
        {
            if (ModelState.IsValid)
            {
                db.m_persons.Add(m_persons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_persons);
        }

        // GET: m_persons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_persons m_persons = db.m_persons.Find(id);
            if (m_persons == null)
            {
                return HttpNotFound();
            }
            return View(m_persons);
        }

        // POST: m_persons/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,personId,personName,mailAddress,phoneNumber,detail,createdDatetime,updatedDatetime")] m_persons m_persons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_persons).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_persons);
        }

        // GET: m_persons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_persons m_persons = db.m_persons.Find(id);
            if (m_persons == null)
            {
                return HttpNotFound();
            }
            return View(m_persons);
        }

        // POST: m_persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_persons m_persons = db.m_persons.Find(id);
            db.m_persons.Remove(m_persons);
            db.SaveChanges();
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

        public ActionResult DetailIndexParts(int id)
        {
            m_persons query = new m_persons();
            using (var db = new MyDbContext())
            {
                try
                {
                    query = db.m_persons.FirstOrDefault(a => a.Id == id);
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessage = "";
                    ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList()
                        .ForEach(model => errorMessage = errorMessage + model.PropertyName + " - " + model.ErrorMessage);
                }
            }

            return PartialView("_DetailIndexParts", query);
        }

        public ActionResult DetailIndex()
        {
            List<m_persons> query = new List<m_persons>();
            using (var db = new MyDbContext())
            {
                try
                {
                    query = db.m_persons.Take(10).ToList();
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessage = "";
                    ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList()
                        .ForEach(model => errorMessage = errorMessage + model.PropertyName + " - " + model.ErrorMessage);
                }
            }

            ViewData["names"] = query;

            return View();
        }

    }
}
