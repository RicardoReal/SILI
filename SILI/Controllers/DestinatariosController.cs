using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;

namespace SILI.Controllers
{
    [Authorize]
    public class DestinatariosController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: Destinatarios
        public async Task<ActionResult> Index()
        {
            var destinatario = db.Destinatario.Include(d => d.Morada).Include(d => d.CodigoPostal);
            return View(await destinatario.ToListAsync());
        }

        // GET: Destinatarios/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destinatario destinatario = await db.Destinatario.FindAsync(id);
            if (destinatario == null)
            {
                return HttpNotFound();
            }
            return View(destinatario);
        }

        // GET: Destinatarios/Create
        public ActionResult Create()
        {
            ViewBag.MoradaID = new SelectList(db.Morada, "ID", null);
            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", null);
            return View();
        }

        // POST: Destinatarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MoradaID,NIFDestinatario,CodPostalID,CodigoDestinatario,CodigoSAP")] Destinatario destinatario)
        {
            if (ModelState.IsValid)
            {
                db.Destinatario.Add(destinatario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MoradaID = new SelectList(db.Morada, "ID", null, destinatario.MoradaID);
            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", null, destinatario.CodPostalID);
            return View(destinatario);
        }

        // GET: Destinatarios/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destinatario destinatario = await db.Destinatario.FindAsync(id);
            if (destinatario == null)
            {
                return HttpNotFound();
            }
            ViewBag.MoradaID = new SelectList(db.Morada, "ID", null, destinatario.MoradaID);
            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", null, destinatario.CodPostalID);
            return View(destinatario);
        }

        // POST: Destinatarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MoradaID,NIFDestinatario,CodPostalID,CodigoDestinatario,CodigoSAP")] Destinatario destinatario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destinatario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MoradaID = new SelectList(db.Morada, "ID", null, destinatario.MoradaID);
            ViewBag.CodPostalID = new SelectList(db.CodigoPostal, "ID", null, destinatario.CodPostalID);
            return View(destinatario);
        }

        // GET: Destinatarios/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destinatario destinatario = await db.Destinatario.FindAsync(id);
            if (destinatario == null)
            {
                return HttpNotFound();
            }
            return View(destinatario);
        }

        // POST: Destinatarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Destinatario destinatario = await db.Destinatario.FindAsync(id);
            db.Destinatario.Remove(destinatario);
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
