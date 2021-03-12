using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using aspcrud1.Models;
using System.Net.Http;
using System.Web.Helpers;
using System.Net.Http.Headers;
using System.Configuration;


namespace aspcrud1.Controllers
{
    public class HomeController : Controller
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["conexionapi"];

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
            /*
            mPersonas Persona = new mPersonas();
            var x = Persona.insertPersona(newPersona);
            */
            return Json(true);
        }


        public JsonResult DetallesPersona(int Id)
        {
            /*
            mPersonas Persona = new mPersonas();
            var x = Persona.PersonasBusqueda(Id);
            return Json(x);
            */
            using (var client = new HttpClient())
            {
                var data = new datos // formato en json para enviar los datos
                {
                    IdJson = Id
                };


                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //Look for the name in the connectionStrings section.
                string path_server = "";
                // If found, return the connection string.
                if (settings != null)
                    path_server = settings.ConnectionString;
                ;

                var request = client.PostAsync(path_server + "Procedimiento_2", content);
                request.Wait();
                var response = request.Result.Content.ReadAsStringAsync().Result;
                var aux = JsonConvert.DeserializeObject(response);
               
               
            return Json(aux);
            }
        }

        public JsonResult GuardarPersona(mPersonas newPersona)
        {
            
            /*
            mPersonas Persona = new mPersonas();
            var x = Persona.GuardarP(newPersona);
            return Json(x);
            */

            using (var client = new HttpClient())
            {

                var data = new datos // formato en json para enviar los datos
                {
                    Nombre = newPersona.Nombre,
                    ApellidoP = newPersona.ApellidoP,
                    ApellidoM = newPersona.ApellidoM,
                    Direccion = newPersona.Direccion,
                    Telefono = newPersona.Telefono
                };


                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //Look for the name in the connectionStrings section.
                string path_server = "";
                // If found, return the connection string.
                if (settings != null)
                    path_server = settings.ConnectionString;
;

                var request = client.PostAsync(path_server + "Insertar", content);
                request.Wait();
                var response = request.Result.Content.ReadAsStringAsync().Result;
                var aux = JsonConvert.DeserializeObject(response);


                return Json(aux);
            }
        }
    

        public JsonResult EditarPersona(mPersonas newPersona)
        {
            /*
            mPersonas Persona = new mPersonas();
            var x = Persona.EditarP(newPersona);*/
            return Json(true);
          

        }




        public JsonResult EliminarPersona(int Id)
        {
            /*
            mPersonas Persona = new mPersonas();
            var x = Persona.CambiarStatus(Id);
            return Json(x);
            */

            using (var client = new HttpClient())
            {

                var data = new datos // formato en json para enviar los datos
                {
                    IdJson = Id
                };


                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                //Look for the name in the connectionStrings section.
                string path_server = "";
                // If found, return the connection string.
                if (settings != null)
                    path_server = settings.ConnectionString;
                ;

                var request = client.PostAsync(path_server + "Borrar", content);
                request.Wait();
                var response = request.Result.Content.ReadAsStringAsync().Result;
                var aux = JsonConvert.DeserializeObject(response);
               
                    return Json(aux);
                
            }
        }
/*
        public JsonResult RecuperarPersona(int Id)
        {

            mPersonas Persona = new mPersonas();
            var x = Persona.RPersona(Id);
            
            return Json(true);
            
        }
 */


    }
}