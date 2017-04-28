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
        private AlumnoCurso alumno_curso = new AlumnoCurso();
        private Curso curso = new Curso();

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

         //home/ver/?Alumno_id=1
        public PartialViewResult Cursos(int Alumno_id)
        {
            //Listamos los cursos de un alumno
            ViewBag.CursosElegidos = alumno_curso.Listar(Alumno_id);

            //Listamos todos los cursos Disponibles
            ViewBag.Cursos = curso.Todos(Alumno_id);
            //modelo
            alumno_curso.Alumno_id = Alumno_id;

            return PartialView(alumno_curso);
        }

        //home/guardar
        public JsonResult GuardarCurso(AlumnoCurso model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();

                if (rm.response)
                {
                    rm.function = "CargarCursos()";
                }
            }

            return Json(rm);/*, JsonRequestBehavior.AllowGet*/
        }

        //home/crud
        public ActionResult Crud(int id = 0)
        {
            return View(
                id == 0 ? new Alumno()
                : alumno.Obtener(id)
                );
        }

        //home/guardar
        public JsonResult Guardar(Alumno model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                rm = model.Guardar();

                if (rm.response)
                {
                    rm.href = Url.Content("~/home");
                }
            }

            return Json(rm);/*, JsonRequestBehavior.AllowGet*/
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