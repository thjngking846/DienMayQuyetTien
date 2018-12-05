﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DanhSachSanPhamController : Controller
    {
        private CS4PEntities db = new CS4PEntities();

        // GET: /DanhSachSanPham/
        public ActionResult Index()
        {
            var bangsanphams = db.BangSanPhams.Include(b => b.LoaiSanPham);
            return View(bangsanphams.ToList());
        }

        // GET: /DanhSachSanPham/Details/5
        public FileResult Details(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path, "images");
        }

        // GET: /DanhSachSanPham/Create
        public ActionResult Create()
        {
            ViewBag.Loai_id = new SelectList(db.LoaiSanPhams, "id", "TenLoai");
            return View();
        }

        // POST: /DanhSachSanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BangSanPham model)
        {
            CheckBangSanPham(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.BangSanPhams.Add(model);
                    db.SaveChanges();

                    if (Request.Files["HinhAnh"] != null &&
                        Request.Files["HinhAnh"].ContentLength > 0)
                    {
                        var path = Server.MapPath("~/App_Data");
                        path = System.IO.Path.Combine(path, model.id.ToString());
                        Request.Files["HinhAnh"].SaveAs(path);

                        scope.Complete();
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError("HinhAnh", "Chua chon hinh anh cho san pham");
                }
            }

            ViewBag.Loai_id = new SelectList(db.LoaiSanPhams, "id", "TenLoai", model.Loai_id);
            return View(model);
        }

        private void CheckBangSanPham(BangSanPham model)
        {
            if (model.GiaGoc < 0)
                ModelState.AddModelError("GiaGoc", "Gia goc phai lon hon 0");
            if (model.GiaGoc > model.GiaBan)
                ModelState.AddModelError("GiaBan", "Gia ban phai lon hon gia goc");
            if (model.GiaGoc > model.GiaGop)
                ModelState.AddModelError("GiaGop", "Gia gop phai lon hon gia goc");
            if (model.SoLuongTon < 0)
                ModelState.AddModelError("SoLuongTon", "So luong ton phai lon hon 0");
        }

        // GET: /DanhSachSanPham/Edit/5
        public ActionResult Edit(int id)
        {
            BangSanPham model = db.BangSanPhams.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Loai_id = new SelectList(db.LoaiSanPhams, "id", "TenLoai", model.Loai_id);
            return View(model);
        }

        // POST: /DanhSachSanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BangSanPham model)
        {
            CheckBangSanPham(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();

                    var path = Server.MapPath("~/App_Data");
                    path = path + "/" + model.id;
                    if (Request.Files["HinhAnh"] != null &&
                        Request.Files["HinhAnh"].ContentLength > 0)
                    {
                        Request.Files["HinhAnh"].SaveAs(path);
                    }

                    scope.Complete(); // approve for transaction
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Loai_id = new SelectList(db.LoaiSanPhams, "id", "TenLoai", model.Loai_id);
            return View(model);
        }

        // GET: /DanhSachSanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BangSanPham bangsanpham = db.BangSanPhams.Find(id);
            if (bangsanpham == null)
            {
                return HttpNotFound();
            }
            return View(bangsanpham);
        }

        // POST: /DanhSachSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BangSanPham bangsanpham = db.BangSanPhams.Find(id);
            db.BangSanPhams.Remove(bangsanpham);
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
