using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.IO;

namespace SILI.Controllers
{
    [Authorize]
    public class DetalheRecepcaosController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        public ActionResult Download(long detalheRecepcaoId)
        {
            DetalheRecepcao detalheRecepcao = db.DetalheRecepcao.Where(x => x.ID == detalheRecepcaoId).FirstOrDefault();

            Stream stream = new MemoryStream(FileGenerator.GenerateEtiqueta(detalheRecepcao));

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + detalheRecepcao.NrDetalhe + ".pdf");

            return new FileStreamResult(stream, "application/pdf");
        }

        // GET: DetalheRecepcaos
        public async Task<ActionResult> Index()
        {
            var detalheRecepcao = db.DetalheRecepcao.Include(d => d.Cliente).Include(d => d.TipoDevolucao).OrderByDescending(d => d.ID);
            return View(await detalheRecepcao.ToListAsync());
        }

        public ActionResult GetClientes(string query)
        {
            return Json(Cliente.GetClientes(query), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMoradas(string query)
        {
            return Json(Morada.GetMoradas(query), JsonRequestBehavior.AllowGet);
        }

        // GET: DetalheRecepcaos/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalheRecepcao detalheRecepcao = await db.DetalheRecepcao.FindAsync(id);
            if (detalheRecepcao == null)
            {
                return HttpNotFound();
            }
            return View(detalheRecepcao);
        }

        // GET: DetalheRecepcaos/Create/Id
        [HttpGet]
        public ActionResult Create(long RecepcaoId)
        {
            ViewBag.RecepcaoID = RecepcaoId;
            ViewBag.ClienteId = new SelectList(db.Cliente, "ID", "Nome");
            ViewBag.DevolvedorID = new SelectList(db.Morada, "ID", "Nome");
            ViewBag.TipoRecepcaoId = new SelectList(db.TipoDevolucao, "ID", "Descricao");
            ViewBag.NrDetalhe = DetalheRecepcao.GenerateNrDetalheRecepcao(RecepcaoId);
            return View();
        }

        // POST: DetalheRecepcaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,RecepcaoID,NrDetalhe,ClienteId,NrVolumes,TipoRecepcaoId,NReferencia,NrGuiaTransporte,DevolvedorID")] DetalheRecepcao detalheRecepcao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Recepcao recep = db.Recepcao.Where(x => x.ID == detalheRecepcao.RecepcaoID).FirstOrDefault();

                    recep.NrVolumesRecepcionados = db.DetalheRecepcao.Where(x => x.RecepcaoID == detalheRecepcao.RecepcaoID).AsEnumerable().Sum(r => r.NrVolumes);
                    recep.NrVolumesRecepcionados += detalheRecepcao.NrVolumes;

                    db.DetalheRecepcao.Add(detalheRecepcao);
                    await db.SaveChangesAsync();
                }
                catch (DbEntityValidationException e)
                {
                    string Error = "";
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Error = "Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:";
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Error += Environment.NewLine + "- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage + "";
                        }
                    }
                    ErrorLog.LogError(Error, "", e.StackTrace, "DetalheRecepcaosController::Create :: POST");
                    throw;
                }
                catch (Exception ex)
                {
                    ErrorLog.LogError(ex, "DetalheRecepcaosController :: Create :: POST");
                }
                return RedirectToAction("Edit", "Recepcao", new { id = detalheRecepcao.RecepcaoID });
            }

            ViewBag.DevolvedorID = new SelectList(db.Morada, "ID", "Nome",detalheRecepcao.DevolvedorID);
            ViewBag.ClienteId = new SelectList(db.Cliente, "ID", "Nome", detalheRecepcao.ClienteID);
            ViewBag.TipoRecepcaoID = new SelectList(db.TipoDevolucao, "ID", "Descricao", detalheRecepcao.TipoRecepcaoID);
            ViewBag.NrDetalhe = DetalheRecepcao.GenerateNrDetalheRecepcao(detalheRecepcao.RecepcaoID);
            return View(detalheRecepcao);

        }

        // GET: DetalheRecepcaos/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalheRecepcao detalheRecepcao = await db.DetalheRecepcao.FindAsync(id);
            if (detalheRecepcao == null)
            {
                return HttpNotFound();
            }
            ViewBag.DevolvedorID = new SelectList(db.Morada, "ID", "Nome", detalheRecepcao.DevolvedorID);
            ViewBag.ClienteId = new SelectList(db.Cliente, "ID", "Nome", detalheRecepcao.ClienteID);
            ViewBag.TipoRecepcaoId = new SelectList(db.TipoDevolucao, "ID", "Descricao", detalheRecepcao.TipoRecepcaoID);
            return View(detalheRecepcao);
        }

        // POST: DetalheRecepcaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,RecepcaoId,NrDetalhe,ClienteId,NrVolumes,TipoRecepcaoId,NReferencia,RecepcaoId,NrGuiaTransporte,DevolvedorID")] DetalheRecepcao detalheRecepcao)
        {
            if (ModelState.IsValid)
            {
                Recepcao recep = db.Recepcao.Where(x => x.ID == detalheRecepcao.RecepcaoID).FirstOrDefault();

                recep.NrVolumesRecepcionados = db.DetalheRecepcao.Where(x => x.RecepcaoID == detalheRecepcao.RecepcaoID && x.ID != detalheRecepcao.ID).AsEnumerable().Sum(r => r.NrVolumes);
                recep.NrVolumesRecepcionados += detalheRecepcao.NrVolumes;

                db.Entry(detalheRecepcao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Recepcao", new { id = detalheRecepcao.RecepcaoID });
            }
            ViewBag.DevolvedorID = new SelectList(db.Morada, "ID", "Nome", detalheRecepcao.DevolvedorID);
            ViewBag.ClienteId = new SelectList(db.Cliente, "ID", "Nome", detalheRecepcao.ClienteID);
            ViewBag.TipoRecepcaoId = new SelectList(db.TipoDevolucao, "ID", "Descricao", detalheRecepcao.TipoRecepcaoID);
            return View(detalheRecepcao);
        }

        // GET: DetalheRecepcaos/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalheRecepcao detalheRecepcao = await db.DetalheRecepcao.FindAsync(id);
            if (detalheRecepcao == null)
            {
                return HttpNotFound();
            }
            return View(detalheRecepcao);
        }

        // POST: DetalheRecepcaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            DetalheRecepcao detalheRecepcao = await db.DetalheRecepcao.FindAsync(id);
            db.DetalheRecepcao.Remove(detalheRecepcao);

            Recepcao recep = db.Recepcao.Where(x => x.ID == detalheRecepcao.RecepcaoID).FirstOrDefault();

            recep.NrVolumesRecepcionados = db.DetalheRecepcao.Where(x => x.RecepcaoID == detalheRecepcao.RecepcaoID).AsEnumerable().Sum(r => r.NrVolumes);
            recep.NrVolumesRecepcionados -= detalheRecepcao.NrVolumes;

            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Recepcao", new { id = detalheRecepcao.RecepcaoID });
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
