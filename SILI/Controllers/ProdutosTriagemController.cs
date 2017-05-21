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
    [Authorize]
    public class ProdutosTriagemController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();
        private static long _triagemID;

        // GET: ProdutosTriagem
        public async Task<ActionResult> Index()
        {
            var produtoTriagem = db.ProdutoTriagem.Include(p => p.MotivoDevolucao).Include(p => p.Produto).Include(p => p.Tratamento);
            return View(await produtoTriagem.ToListAsync());
        }

        public ActionResult GetProdutos(string query)
        {
            return Json(Produto.GetProdutos(query), JsonRequestBehavior.AllowGet);
        }

        // GET: ProdutosTriagem/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoTriagem produtoTriagem = await db.ProdutoTriagem.FindAsync(id);
            if (produtoTriagem == null)
            {
                return HttpNotFound();
            }
            return View(produtoTriagem);
        }

        // GET: ProdutosTriagem/Create
        public ActionResult Create(long TriagemID)
        {
            _triagemID = TriagemID;
            ViewBag.TriagemID = TriagemID;
            ViewBag.MotivoDevolucaoID = new SelectList(db.MotivoDevolucao, "ID", "Motivos");
            ViewBag.EANCNP = new SelectList(db.Produto, "ID", "Referencia");
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao");
            return View();
        }

        // POST: ProdutosTriagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,TriagemID,EANCNP,QtdDevolvida,Lote,PVP,MotivoDevolucaoID,TratamentoID")] ProdutoTriagem produtoTriagem)
        {
            if (ModelState.IsValid)
            {
                produtoTriagem.TriagemID = _triagemID;

                db.ProdutoTriagem.Add(produtoTriagem);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }

            ViewBag.MotivoDevolucaoID = new SelectList(db.MotivoDevolucao, "ID", "Motivos", produtoTriagem.MotivoDevolucaoID);
            ViewBag.EANCNP = new SelectList(db.Produto, "ID", "Referencia", produtoTriagem.EANCNP);
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao", produtoTriagem.TratamentoID);
            //return View(produtoTriagem);
            return RedirectToAction("Create", "ProdutosTriagem", new { @TriagemID = _triagemID });
        }



        // GET: ProdutosTriagem/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoTriagem produtoTriagem = await db.ProdutoTriagem.FindAsync(id);
            if (produtoTriagem == null)
            {
                return HttpNotFound();
            }
            ViewBag.MotivoDevolucaoID = new SelectList(db.MotivoDevolucao, "ID", "Motivos", produtoTriagem.MotivoDevolucaoID);
            ViewBag.EANCNP = new SelectList(db.Produto, "ID", "Referencia", produtoTriagem.EANCNP);
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao", produtoTriagem.TratamentoID);
            return View(produtoTriagem);
        }

        // POST: ProdutosTriagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,TriagemID,EANCNP,QtdDevolvida,Lote,PVP,MotivoDevolucaoID,TratamentoID")] ProdutoTriagem produtoTriagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produtoTriagem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MotivoDevolucaoID = new SelectList(db.MotivoDevolucao, "ID", "Motivos", produtoTriagem.MotivoDevolucaoID);
            ViewBag.EANCNP = new SelectList(db.Produto, "ID", "Referencia", produtoTriagem.EANCNP);
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao", produtoTriagem.TratamentoID);
            return View(produtoTriagem);
        }

        // GET: ProdutosTriagem/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoTriagem produtoTriagem = await db.ProdutoTriagem.FindAsync(id);
            if (produtoTriagem == null)
            {
                return HttpNotFound();
            }
            return View(produtoTriagem);
        }

        // POST: ProdutosTriagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ProdutoTriagem produtoTriagem = await db.ProdutoTriagem.FindAsync(id);
            db.ProdutoTriagem.Remove(produtoTriagem);
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
