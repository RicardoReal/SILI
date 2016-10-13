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

namespace SILI.Controllers
{
    public class TipoDevolvedorsController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: TipoDevolvedors
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoDevolvedor.ToListAsync());
        }

        // GET: TipoDevolvedors/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDevolvedor tipoDevolvedor = await db.TipoDevolvedor.FindAsync(id);
            if (tipoDevolvedor == null)
            {
                return HttpNotFound();
            }
            return View(tipoDevolvedor);
        }

        // GET: TipoDevolvedors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDevolvedors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Numero,Descricao")] TipoDevolvedor tipoDevolvedor)
        {
            if (ModelState.IsValid)
            {
                db.TipoDevolvedor.Add(tipoDevolvedor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoDevolvedor);
        }

        // GET: TipoDevolvedors/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDevolvedor tipoDevolvedor = await db.TipoDevolvedor.FindAsync(id);
            if (tipoDevolvedor == null)
            {
                return HttpNotFound();
            }
            return View(tipoDevolvedor);
        }

        // POST: TipoDevolvedors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Numero,Descricao")] TipoDevolvedor tipoDevolvedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDevolvedor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoDevolvedor);
        }

        // GET: TipoDevolvedors/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDevolvedor tipoDevolvedor = await db.TipoDevolvedor.FindAsync(id);
            if (tipoDevolvedor == null)
            {
                return HttpNotFound();
            }
            return View(tipoDevolvedor);
        }

        // POST: TipoDevolvedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            TipoDevolvedor tipoDevolvedor = await db.TipoDevolvedor.FindAsync(id);
            db.TipoDevolvedor.Remove(tipoDevolvedor);
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
