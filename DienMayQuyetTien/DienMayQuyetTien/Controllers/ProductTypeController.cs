using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayQuyetTien.Models;

namespace DienMayQuyetTien.Controllers
{
    public class ProductTypeController : Controller
    {
        private CS4PEEntities db = new CS4PEEntities();

        // GET: /ProductType/
        public ActionResult Index()
        {
            return View(db.LoaiSanPhams.ToList());
        }

        // GET: /ProductType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaisanpham = db.LoaiSanPhams.Find(id);
            if (loaisanpham == null)
            {
                return HttpNotFound();
            }
            return View(loaisanpham);
        }

        // GET: /ProductType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ProductType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,TenLoai")] LoaiSanPham loaisanpham)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSanPhams.Add(loaisanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaisanpham);
        }

        // GET: /ProductType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaisanpham = db.LoaiSanPhams.Find(id);
            if (loaisanpham == null)
            {
                return HttpNotFound();
            }
            return View(loaisanpham);
        }

        // POST: /ProductType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,TenLoai")] LoaiSanPham loaisanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaisanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaisanpham);
        }

        // GET: /ProductType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaisanpham = db.LoaiSanPhams.Find(id);
            if (loaisanpham == null)
            {
                return HttpNotFound();
            }
            return View(loaisanpham);
        }

        // POST: /ProductType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSanPham loaisanpham = db.LoaiSanPhams.Find(id);
            db.LoaiSanPhams.Remove(loaisanpham);
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
    }
}
