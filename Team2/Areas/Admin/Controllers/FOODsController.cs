using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Team2.Models;

namespace Team2.Areas.Admin.Controllers
{
    public class FOODsController : Controller
    {
        private QUANLYCANTEENEntities db = new QUANLYCANTEENEntities();

        // GET: Admin/FOODs
        public ActionResult Index()
        {
            var fOODs = db.FOODs.Include(f => f.CATEGORY);
            return View(fOODs.ToList());
        }

        // GET: Admin/FOODs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOOD fOOD = db.FOODs.Find(id);
            if (fOOD == null)
            {
                return HttpNotFound();
            }
            return View(fOOD);
        }

        // GET: Admin/FOODs/Create
        public ActionResult Create()
        {
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORies, "ID", "CATEGORY_NAME");
            return View();
        }

        // POST: Admin/FOODs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FOOD fOOD, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        fOOD.STATUS = true;
                        db.FOODs.Add(fOOD);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        Picture.SaveAs(path + fOOD.ID);

                        scope.Complete();
                        return RedirectToAction("Index");
                    }
                }
                else ModelState.AddModelError("", "Picture not found!");
            }

            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORies, "ID", "CATEGORY_NAME", fOOD.CATEGORY_ID);
            return View(fOOD);
        }

        // GET: Admin/FOODs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOOD fOOD = db.FOODs.Find(id);
            if (fOOD == null)
            {
                return HttpNotFound();
            }
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORies, "ID", "CATEGORY_CODE", fOOD.CATEGORY_ID);
            return View(fOOD);
        }

        // POST: Admin/FOODs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FOOD fOOD, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(fOOD).State = EntityState.Modified;
                    db.SaveChanges();

                    if (Picture != null)
                    {
                        var path = Server.MapPath(PICTURE_PATH);
                        Picture.SaveAs(path + fOOD.ID);
                    }

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORies, "ID", "CATEGORY_CODE", fOOD.CATEGORY_ID);
            return View(fOOD);
        }

        // GET: Admin/FOODs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FOOD fOOD = db.FOODs.Find(id);
            if (fOOD == null)
            {
                return HttpNotFound();
            }
            return View(fOOD);
        }

        // POST: Admin/FOODs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var scope = new TransactionScope())
            {
                var model = db.FOODs.Find(id);
                db.FOODs.Remove(model);
                db.SaveChanges();

                var path = Server.MapPath(PICTURE_PATH);
                System.IO.File.Delete(path + model.ID);

                scope.Complete();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Image(int id)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + id, "images");
        }

        private const string PICTURE_PATH = "~/Images/Foods/";

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
