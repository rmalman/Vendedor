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
    public class UsuariosController : Controller
    {
        private VendedorContext db = new VendedorContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.DadoPessoaFisica);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.DadosPessoaFisicaID = new SelectList(db.PessoaFisica, "DadosPessoaFisicaID", "Nome", db.Endereco, "EnderecoID");
            var Estados = from e in db.Estados
                          select e;
            Estados = Estados.OrderBy(e => e.Nome);

            ViewBag.EstadoID = new SelectList(Estados, "EstadoID", "Nome");

            //Obtem Todos Municipios
            List<Municipio> Cidades = new List<Municipio>();
            Cidades = db.Municipios.OrderBy(e => e.Nome).Where(c => c.EstadoID.Equals(-1)).ToList();

            ViewBag.CidadeID = new SelectList(Cidades, "MunicipioID", "Nome");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.DadoPessoaFisica.UsuarioID = usuario.UsuarioID;                
                
                db.Usuarios.Add(usuario);
                
                db.PessoaFisica.Add(usuario.DadoPessoaFisica);
                db.SaveChanges();

                ModelState.Clear();
                return RedirectToAction("Index");
            }

            ViewBag.DadosPessoaFisicaID = new SelectList(db.PessoaFisica, "DadosPessoaFisicaID", "Nome", db.Endereco, "EnderecoID");
            
            //Obtem todos Estados
            List<Estado> Estados = new List<Estado>();

            Estados = db.Estados.OrderBy(e => e.Nome).ToList();

            string UF = Request.Form["DadosPF_EstadoID"].ToString();

            ViewBag.EstadoID = new SelectList(Estados, "EstadoID", "Nome", usuario.DadoPessoaFisica.Endereco.EstadoID);
            //ViewBag.EstadoID = new SelectList(Estados, "EstadoID", "Nome");

            //Obtem Todos Municipios
            List<Municipio> Cidades = new List<Municipio>();
            Cidades = db.Municipios.OrderBy(e => e.Nome).ToList();
            if (usuario.DadoPessoaFisica.Endereco.EstadoID != null && usuario.DadoPessoaFisica.Endereco.EstadoID > 0)            
            {
                Cidades = Cidades.Where(c => c.EstadoID.Equals(usuario.DadoPessoaFisica.Endereco.EstadoID)).OrderBy(c => c.Nome).ToList();
            }

            ViewBag.CidadeID = new SelectList(Cidades, "MunicipioID", "Nome", usuario.DadoPessoaFisica.Endereco.MunicipioID);

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.DadosPessoaFisicaID = new SelectList(db.PessoaFisica, "DadosPessoaFisicaID", "Nome", usuario.DadosPessoaFisicaID);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioID,Login,Senha,Email,DadosPessoaFisicaID,DadoPessoaFisica")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                //DadosPessoaFisica pessoaFisica = usuario.DadoPessoaFisica;
                db.Entry(usuario).State = EntityState.Modified;
                //db.Entry(pessoaFisica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DadosPessoaFisicaID = new SelectList(db.PessoaFisica, "DadosPessoaFisicaID", "Nome", usuario.DadosPessoaFisicaID);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult GetCities(String EstadoID)
        {
            int ID = 0;
            var Tabela = from m in db.Municipios
                         select m;


            List<Municipio> Cidades = new List<Municipio>();
            if (int.TryParse(EstadoID, out ID))
            {
                Tabela = Tabela.Where(t => t.EstadoID.Equals(ID)).OrderBy(t => t.Nome);
            }
                        
            Cidades = Tabela.ToList();
            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = Cidades,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Requisição Inválida em Cidades.",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }


    }
}
