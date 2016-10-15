using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vendedor.DAL;
using Vendedor.Models;

namespace Vendedor.Controllers
{
    public class MunicipioController : Controller
    {
        private VendedorContext db = new VendedorContext();

        // GET: Municipio
        public ActionResult Index(string sortOrder, int Estado = 0)
        {
            var municipios = db.Municipios.Include(m => m.UF);
            var Estados = from e in db.Estados
                          select e;
            Estados = Estados.OrderBy(e => e.Nome);

            ViewBag.EstadoID = new SelectList(Estados, "EstadoID", "Nome");
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? (sortOrder == "UF" ? "name_desc" : "UF") : "";
            ViewBag.Indice = "Indice";
            var Tabela = from t in db.Municipios
                         select t;
            switch (sortOrder)
            {
                case "name_desc":
                    Tabela = Tabela.OrderByDescending(t => t.Nome);
                    break;
                case "UF":
                    Tabela = Tabela.OrderBy(t => t.UF.Nome);
                    break;
                default:
                    Tabela = Tabela.OrderBy(t => t.Nome);
                    break;
            }

            if (Estado > 0)
            {
                Tabela = Tabela.Where(t => t.UF.EstadoID.Equals(Estado));
            }


            return View(Tabela.ToList());
        }

        // GET: Municipio/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipios.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        // GET: Municipio/Create
        public ActionResult Create()
        {
            var Estados = from e in db.Estados
                          select e;
            Estados = Estados.OrderBy(e => e.Nome);

            ViewBag.EstadoID = new SelectList(Estados, "EstadoID", "Nome");
            return View();
        }

        // POST: Municipio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MunicipioID,EstadoID,Nome")] Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                db.Municipios.Add(municipio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var Estados = from e in db.Estados
                          select e;
            Estados = Estados.OrderBy(e => e.Nome);

            ViewBag.EstadoID = new SelectList(Estados, "EstadoID", "Nome", municipio.EstadoID);
            return View(municipio);
        }

        // GET: Municipio/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipios.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "Nome", municipio.EstadoID);
            return View(municipio);
        }

        // POST: Municipio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MunicipioID,EstadoID,Nome")] Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoID = new SelectList(db.Estados, "EstadoID", "Nome", municipio.EstadoID);
            return View(municipio);
        }

        // GET: Municipio/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Municipio municipio = db.Municipios.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        // POST: Municipio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Municipio municipio = db.Municipios.Find(id);
            db.Municipios.Remove(municipio);
            db.SaveChanges();
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

        public JsonResult GetCities(int EstadoID)
        {
            var Tabela = from t in db.Municipios
                         select t;
            if (EstadoID > 0)
            {
                Tabela = Tabela.Where(t => t.UF.EstadoID.Equals(EstadoID));
            }


            //return View(Tabela.ToList());
            List<Municipio> Cidades = new List<Municipio>();
            Cidades = Tabela.ToList();
            return Json(Cidades, JsonRequestBehavior.AllowGet);
        }
    }
}
