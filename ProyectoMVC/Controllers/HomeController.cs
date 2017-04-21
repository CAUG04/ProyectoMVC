using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace ProyectoMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           return View();
        }
        public ActionResult Ver()
        {
            return View(Alumno.Obtener());
        }
       public ActionResult Guardar(Alumno alumno)
        {
            return Redirect("~/Home/Index");
        }
    }
}