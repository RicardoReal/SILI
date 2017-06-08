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
    public class UserRolesController : Controller
    {
        private SILI_DBEntities db = new SILI_DBEntities();

        // GET: UserRoles
        public async Task<ActionResult> Index()
        {
            var userRole = db.UserRole.Include(u => u.Role).Include(u => u.User);
            return View(await userRole.ToListAsync());
        }

        // GET: UserRoles/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = await db.UserRole.FindAsync(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        //// GET: UserRoles/Create
        //public ActionResult Create()
        //{
        //    ViewBag.RoleId = new SelectList(db.Role, "ID", "Nome");
        //    ViewBag.UserId = new SelectList(db.User, "ID", "FirstName");
        //    return View();
        //}

        // GET: UserRoles/Create/Id
        [HttpGet]
        public ActionResult Create(long UserId)
        {
            ViewBag.UserId = UserId;
            ViewBag.RoleId = new SelectList(db.Role, "ID", "Nome");
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,UserId,RoleId")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                if (db.UserRole.Where(x => x.UserId == userRole.UserId && x.RoleId == userRole.RoleId).Count() > 0)
                {
                    ModelState.AddModelError("", "O utilizador já tem associado o role seleccionado.");
                }
                else
                {
                    db.UserRole.Add(userRole);
                    await db.SaveChangesAsync();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Edit", "Users", new { id = userRole.UserId });
                }
            }

            ViewBag.RoleId = new SelectList(db.Role, "ID", "Nome", userRole.RoleId);
            ViewBag.UserId = new SelectList(db.User, "ID", "FirstName", userRole.UserId);
            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = await db.UserRole.FindAsync(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Role, "ID", "Nome", userRole.RoleId);
            ViewBag.UserId = new SelectList(db.User, "ID", "FirstName", userRole.UserId);
            return View(userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,UserId,RoleId")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                if (db.UserRole.Where(x => x.UserId == userRole.UserId && x.RoleId == userRole.RoleId && x.ID != userRole.ID).Count() > 0)
                {
                    ModelState.AddModelError("", "O utilizador já tem associado o role seleccionado.");
                }
                else
                {
                    db.Entry(userRole).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Edit", "Users", new { id = userRole.UserId });
                }
            }
            ViewBag.RoleId = new SelectList(db.Role, "ID", "Nome", userRole.RoleId);
            ViewBag.UserId = new SelectList(db.User, "ID", "FirstName", userRole.UserId);
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = await db.UserRole.FindAsync(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            UserRole userRole = await db.UserRole.FindAsync(id);
            db.UserRole.Remove(userRole);
            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Users", new { id = userRole.UserId });
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
