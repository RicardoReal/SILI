using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Data.SqlClient;
using System;
using System.Data.Entity.Infrastructure;

namespace SILI.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: Clientes
        public async Task<ActionResult> Index()
        {
            return View(await db.Cliente.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NrInterno,Nome,Morada,NIF,Email,NrContacto,NomeContacto")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NrInterno,Nome,Morada,NIF,Email,NrContacto,NomeContacto")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Cliente cliente = await db.Cliente.FindAsync(id);
            try
            {
                db.Cliente.Remove(cliente);
                await db.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                ModelState.AddModelError("", "Não é possivel apagar clientes que estejam a ser referenciados.");
                return View(cliente);
            }
            
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
