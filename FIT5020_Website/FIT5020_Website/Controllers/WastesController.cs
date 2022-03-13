using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5020_Website.Models;

namespace FIT5020_Website.Controllers
{
    public class WastesController : Controller
    {
        private wasteSortContainer db = new wasteSortContainer();

        // GET: Wastes
        public ActionResult Index()
        {
            return View(db.WasteSet.ToList());
        }

        // GET: Wastes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waste waste = db.WasteSet.Find(id);
            if (waste == null)
            {
                return HttpNotFound();
            }
            return View(waste);
        }

        // GET: Wastes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wastes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,BinType")] Waste waste)
        {
            if (ModelState.IsValid)
            {
                db.WasteSet.Add(waste);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(waste);
        }

        // GET: Wastes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waste waste = db.WasteSet.Find(id);
            if (waste == null)
            {
                return HttpNotFound();
            }
            return View(waste);
        }

        // POST: Wastes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,BinType")] Waste waste)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waste).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(waste);
        }

        // GET: Wastes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waste waste = db.WasteSet.Find(id);
            if (waste == null)
            {
                return HttpNotFound();
            }
            return View(waste);
        }

        // POST: Wastes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Waste waste = db.WasteSet.Find(id);
            db.WasteSet.Remove(waste);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string key)
        {
            var wastes = from waste in db.WasteSet.ToList()
                           select waste;
            if (!String.IsNullOrEmpty(key))
            {
                key = key.Trim();
                wastes = wastes.Where(a => a.Name.Contains(key));
            }
            return View("Index", wastes);
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
