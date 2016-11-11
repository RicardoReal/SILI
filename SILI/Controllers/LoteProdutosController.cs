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
    public class LoteProdutosController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: LoteProdutos
        public async Task<ActionResult> Index()
        {
            var loteProduto = db.LoteProduto.Include(l => l.Produto).Include(l => l.Tratamento).Include(l => l.User);
            return View(await loteProduto.ToListAsync());
        }

        // GET: LoteProdutos/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoteProduto loteProduto = await db.LoteProduto.FindAsync(id);
            if (loteProduto == null)
            {
                return HttpNotFound();
            }
            return View(loteProduto);
        }

        // GET: LoteProdutos/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ProdutoID = new SelectList(db.Produto, "ID", "Referencia");
        //    ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao");
        //    ViewBag.ActualizadoPor = new SelectList(db.User, "ID", "FirstName");
        //    return View();
        //}

        // GET: LoteProdutos/Create/Id
        public ActionResult Create(long ProdutoId)
        {
            ViewBag.ProdutoID = new SelectList(db.Produto.Where(p => p.ID == ProdutoId).ToList(), "ID", "Referencia");
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao");
            ViewBag.ActualizadoPor = new SelectList(db.User, "ID", "FirstName");
            return View();
        }

        // POST: LoteProdutos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ProdutoID,Lote,Validade,Preco,TratamentoID,DataAlteracao,ActualizadoPor")] LoteProduto loteProduto)
        {
            if (ModelState.IsValid)
            {
                loteProduto.DataAlteracao = DateTime.Now;

                db.LoteProduto.Add(loteProduto);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Produtos", new { id = loteProduto.ProdutoID });
            }

            ViewBag.ProdutoID = new SelectList(db.Produto.Where(p => p.ID == loteProduto.ProdutoID).ToList(), "ID", "FormattedToString", loteProduto.ProdutoID);
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao", loteProduto.TratamentoID);
            ViewBag.ActualizadoPor = new SelectList(db.User, "ID", "FirstName", loteProduto.ActualizadoPor);
            return View(loteProduto);
        }

        // GET: LoteProdutos/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoteProduto loteProduto = await db.LoteProduto.FindAsync(id);
            if (loteProduto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdutoID = new SelectList(db.Produto.Where(p => p.ID == loteProduto.ProdutoID).ToList(), "ID", "FormattedToString", loteProduto.ProdutoID);
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao", loteProduto.TratamentoID);
            ViewBag.ActualizadoPor = new SelectList(db.User, "ID", "FirstName", loteProduto.ActualizadoPor);
            return View(loteProduto);
        }

        // POST: LoteProdutos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ProdutoID,Lote,Validade,Preco,TratamentoID,DataAlteracao,ActualizadoPor")] LoteProduto loteProduto)
        {
            if (ModelState.IsValid)
            {
                loteProduto.DataAlteracao = DateTime.Now;

                db.Entry(loteProduto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", "Produtos", new { id = loteProduto.ProdutoID });
            }
            ViewBag.ProdutoID = new SelectList(db.Produto, "ID", "Referencia", loteProduto.ProdutoID);
            ViewBag.TratamentoID = new SelectList(db.Tratamento, "ID", "Descricao", loteProduto.TratamentoID);
            ViewBag.ActualizadoPor = new SelectList(db.User, "ID", "FirstName", loteProduto.ActualizadoPor);
            return View(loteProduto);
        }

        // GET: LoteProdutos/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoteProduto loteProduto = await db.LoteProduto.FindAsync(id);
            if (loteProduto == null)
            {
                return HttpNotFound();
            }
            return View(loteProduto);
        }

        // POST: LoteProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            LoteProduto loteProduto = await db.LoteProduto.FindAsync(id);
            db.LoteProduto.Remove(loteProduto);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Produtos", new { id = loteProduto.ProdutoID });
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
