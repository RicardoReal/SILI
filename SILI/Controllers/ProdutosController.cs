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
    public class ProdutosController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: Produtos
        public async Task<ActionResult> Index()
        {
            var produto = db.Produto.Include(p => p.Cliente).Include(p => p.Tipologia);
            return View(await produto.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produto.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nome");
            ViewBag.TipologiaID = new SelectList(db.Tipologia, "ID", "Descricao");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Referencia,EAN,CNP,Descricao,ClienteID,Apresentacao,Largura,Altura,Peso,QtdCaixa,QtdPalete,UndVenda,TipologiaID,PrecoTabelado")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produto.Add(produto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nome", produto.ClienteID);
            ViewBag.TipologiaID = new SelectList(db.Tipologia, "ID", "Descricao", produto.TipologiaID);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produto.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nome", produto.ClienteID);
            ViewBag.TipologiaID = new SelectList(db.Tipologia, "ID", "Descricao", produto.TipologiaID);

            return View(produto);
        }

        public ActionResult GetLoteProduto(long id)
        {
            ViewData["ProdutoId"] = id;
            return PartialView("LoteProdutosList", db.LoteProduto.Where(x => x.ProdutoID == id).ToList());
        }
        

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Referencia,EAN,CNP,Descricao,ClienteID,Apresentacao,Largura,Altura,Peso,QtdCaixa,QtdPalete,UndVenda,TipologiaID,PrecoTabelado")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nome", produto.ClienteID);
            ViewBag.TipologiaID = new SelectList(db.Tipologia, "ID", "Descricao", produto.TipologiaID);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produto.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Produto produto = await db.Produto.FindAsync(id);
            db.Produto.Remove(produto);
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
