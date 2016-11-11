﻿using System;
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
    public class RecepcaoController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: Recepcao
        public async Task<ActionResult> Index()
        {
            var recepcao = db.Recepcao.Include(r => r.Morada).Include(r => r.User);
            return View(await recepcao.ToListAsync());
        }

        // GET: Recepcao/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcao recepcao = await db.Recepcao.FindAsync(id);
            if (recepcao == null)
            {
                return HttpNotFound();
            }
            return View(recepcao);
        }

        // GET: Recepcao/Create
        public ActionResult Create()
        {
            ViewBag.EntreguePor = new SelectList(db.Morada, "ID", "Nome");
            ViewBag.Colaborador = new SelectList(db.User, "ID", "FirstName");
            return View();
        }

        // POST: Recepcao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NrRecepcao,DataHora,DataHoraChegadaArmazem,EntreguePor,NrGuiaTransportador,NrVolumesRecepcionados,NrVolumesGuia,Colaborador")] Recepcao recepcao)
        {
            if (ModelState.IsValid)
            {
                recepcao.NrRecepcao = recepcao.GenerateNrRecepcao();
                recepcao.DataHora = DateTime.Now;

                db.Recepcao.Add(recepcao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EntreguePor = new SelectList(db.Morada, "ID", "Nome", recepcao.EntreguePor);
            ViewBag.Colaborador = new SelectList(db.User, "ID", "FirstName", recepcao.Colaborador);
            return View(recepcao);
        }

        // GET: Recepcao/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcao recepcao = await db.Recepcao.FindAsync(id);
            if (recepcao == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntreguePor = new SelectList(db.Morada, "ID", "Nome", recepcao.EntreguePor);
            ViewBag.Colaborador = new SelectList(db.User, "ID", "FirstName", recepcao.Colaborador);
            return View(recepcao);
        }

        // POST: Recepcao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NrRecepcao,DataHora,DataHoraChegadaArmazem,EntreguePor,NrGuiaTransportador,NrVolumesRecepcionados,NrVolumesGuia,Colaborador")] Recepcao recepcao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recepcao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EntreguePor = new SelectList(db.Morada, "ID", "Nome", recepcao.EntreguePor);
            ViewBag.Colaborador = new SelectList(db.User, "ID", "FirstName", recepcao.Colaborador);
            return View(recepcao);
        }

        // GET: Recepcao/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recepcao recepcao = await db.Recepcao.FindAsync(id);
            if (recepcao == null)
            {
                return HttpNotFound();
            }
            return View(recepcao);
        }

        // POST: Recepcao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Recepcao recepcao = await db.Recepcao.FindAsync(id);
            db.Recepcao.Remove(recepcao);
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