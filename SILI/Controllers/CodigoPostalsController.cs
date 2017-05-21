using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SILI;
using System.Text.RegularExpressions;

namespace SILI.Controllers
{
    [Authorize]
    public class CodigoPostalsController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: CodigoPostals
        public async Task<ActionResult> Index()
        {
            return View(await db.CodigoPostal.ToListAsync());
        }

        // GET: CodigoPostals/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodigoPostal codigoPostal = await db.CodigoPostal.FindAsync(id);
            if (codigoPostal == null)
            {
                return HttpNotFound();
            }
            return View(codigoPostal);
        }

        // GET: CodigoPostals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodigoPostals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,CodPostal,Localidade,Distrito")] CodigoPostal codigoPostal)
        {
            if (ModelState.IsValid)
            {
                db.CodigoPostal.Add(codigoPostal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(codigoPostal);
        }

        

        // GET: CodigoPostals/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodigoPostal codigoPostal = await db.CodigoPostal.FindAsync(id);
            if (codigoPostal == null)
            {
                return HttpNotFound();
            }
            return View(codigoPostal);
        }

        // POST: CodigoPostals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CodPostal,Localidade,Distrito")] CodigoPostal codigoPostal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codigoPostal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(codigoPostal);
        }

        // GET: CodigoPostals/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodigoPostal codigoPostal = await db.CodigoPostal.FindAsync(id);
            if (codigoPostal == null)
            {
                return HttpNotFound();
            }
            return View(codigoPostal);
        }

        // POST: CodigoPostals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            CodigoPostal codigoPostal = await db.CodigoPostal.FindAsync(id);
            db.CodigoPostal.Remove(codigoPostal);
            await db.SaveChangesAsync();
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
