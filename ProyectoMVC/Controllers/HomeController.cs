using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMVC.Controllers
{
    public class HomeController : Controller
    {
        private Alumno alumno = new Alumno();

        // GET: Home
        public ActionResult Index()
        {
            return View(alumno.Listar());
        }

        //Home/Ver/1
        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }

        //Home/Crud
        public ActionResult Crud(int id = 0)
        {
            return View(
                id == 0 ? new Alumno()
                : alumno.Obtener(id)
                );
        }

        //Home/Guardar
        public ActionResult Guardar(Alumno model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/home");
            }
            else
            {
                return View("~/views/home/crud.cshtml", model);
            }
           
        }

        //Home/Eliminar
        public ActionResult Eliminar(int id)
        {
            alumno.id = id;
            alumno.Eliminar();
            return Redirect("~/home");
        }
    }
}