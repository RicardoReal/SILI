using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SILI.Models;
using System.Data.Entity;

namespace SILI.Controllers
{
    [Authorize]
    public class NovaTriagemController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nome");

            return View();
        }

        // POST: NovaTriagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoBarras,ClienteID,NrVolumes")] NovaTriagemModel novaTriagem)
        {
            if (ModelState.IsValid)
            {
                DetalheRecepcao detalhe = db.DetalheRecepcao.Where(dr => dr.NrDetalhe == novaTriagem.CodigoBarras && dr.ClienteId == novaTriagem.ClienteID && dr.NrVolumes == novaTriagem.NrVolumes).FirstOrDefault();

                if (detalhe != null)
                {
                    Triagem triagem = new Triagem();

                    Cliente cliente = db.Cliente.Where(cl => cl.ID == novaTriagem.ClienteID).FirstOrDefault();
                    triagem.NrProcesso = triagem.GenerateNrTriagem(cliente);
                    triagem.DataHoraRecepcao = DateTime.Now;
                    triagem.ColaboradorID = SILI.User.GetUserIdByUsername(User.Identity.Name);
                    triagem.SubUnidades = false;

                    db.Triagem.Add(triagem);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Edit", "Triagens", new { id = triagem.ID });
                }
                
            }
            ModelState.AddModelError("", "Por favor, confirme os campos introduzidos.");
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nome");
            return View(novaTriagem);
        }
    }
}