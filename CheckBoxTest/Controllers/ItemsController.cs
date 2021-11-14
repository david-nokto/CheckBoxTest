using CheckBoxTest.Data;
using CheckBoxTest.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CheckBoxTest.Controllers
{
    public class ItemsController : Controller
    {
        private CheckBoxTestContext db = new CheckBoxTestContext();

        // GET: Items
        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Status")] Items items)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(items);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Status")] Items items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(items);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Items items = db.Items.Find(id);
            db.Items.Remove(items);
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


        //[AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult RegisterToggle(int? id)
        {
            var item = db.Items.Find(id);

            //if (!item.Status)
            //{
            //    item.Status = false;

            //}
            //else
            //{
            //    item.Status = true;
            //}

            item.Status = item.Status ? false : true;

            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();


            return Content("Status toggled");
        }

        [HttpPost]
        public ActionResult RegisterToggle2(IEnumerable<ToggleDTO> items)
        {


            foreach (var item in items)
            {
                var itemToChange = db.Items.Find(item.Id);
                itemToChange.Status = item.Status ? true : false;
                db.Entry(itemToChange).State = EntityState.Modified;
            }

            db.SaveChanges();



            return Content("Status toggled");
        }

    }
}
