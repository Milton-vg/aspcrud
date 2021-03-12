using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using aspcrud1.Models;

namespace aspcrud1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult TablaPersonas(int Filtro)
        {
            mPersonas Persona = new mPersonas();

            var x = Persona.obtenerPersonas(Filtro);
            return Json(x);
        }

        public JsonResult TablaPersonasbusqueda(string Busqueda)
        {
            mPersonas Persona = new mPersonas();

            var x = Persona.obtenerPersonasBusqueda(Busqueda);
            return Json(x);
        }

        public JsonResult CrearClientes(mPersonas newPersona)
        {

            mPersonas Persona = new mPersonas();
            var x = Persona.insertPersona(newPersona);
            return Json(x);
        }

        public JsonResult DetallesPersona(int Id)
        {
            mPersonas Persona = new mPersonas();

            var x = Persona.PersonasBusqueda(Id);
            return Json(x);

        }

        public JsonResult GuardarPersona(mPersonas newPersona)
        {

            mPersonas Persona = new mPersonas();
            var x = Persona.GuardarP(newPersona);
            return Json(x);
        }

        public JsonResult EditarPersona(mPersonas newPersona)
        {

            mPersonas Persona = new mPersonas();
            var x = Persona.EditarP(newPersona);
            return Json(x);
        }

        public JsonResult EliminarPersona(int Id)
        {

            mPersonas Persona = new mPersonas();
            var x = Persona.CambiarStatus(Id);
            return Json(x);
        }

        public JsonResult RecuperarPersona(int Id)
        {

            mPersonas Persona = new mPersonas();
            var x = Persona.RPersona(Id);
            return Json(x);
        }
    }
}