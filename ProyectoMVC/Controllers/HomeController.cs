using Model;
using System;
using System.Collections.Generic;
using System.IO;
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
        private Adjunto adjunto = new Adjunto();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CargarAlumnos(AnexGRID grid)
        {
            return Json(alumno.Listar(grid));
        }

        //Home/Ver/1
        public ActionResult Ver(int id)
        {
            return View(alumno.Obtener(id));
        }

        //home/cursos/?Alumno_id=1
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

        //home/guardar
        public JsonResult GuardarAdjunto(Adjunto model, HttpPostedFileBase Archivo)
        {
            var rm = new ResponseModel();

            if (Archivo != null/* && Archivo.ContentLength > 0*/)
            {
                // Nombre del archivo es decir, lo renombramos para que no se repita nunca
                string archivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(Archivo.FileName);

                //La ruta donde lo vamos guardar
                Archivo.SaveAs(Server.MapPath("~/uploads/") + archivo);

                //Establecemos en nuestro modelo el nombre del archivo
                model.Archivo = archivo;

                rm = model.Guardar();


                if (rm.response)
                {
                    rm.function = "CargarAdjuntos()";
                }
            }

            rm.SetResponse(false, "Debe adjuntar un archivo");

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

        //home/adjuntos/?Alumno_id=1
        public PartialViewResult Adjuntos(int Alumno_id)
        {
            ViewBag.Adjuntos = adjunto.Listar(Alumno_id);
            return PartialView();
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