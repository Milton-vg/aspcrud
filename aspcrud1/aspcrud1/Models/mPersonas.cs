using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Web;

namespace aspcrud1.Models
{
    public class mPersonas
    {
        SqlClass miSqlClass = new SqlClass();

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Estatus{ get; set; }

        List<mPersonas> lsPersonas = new List<mPersonas>();

        public List<mPersonas> obtenerPersonas(int Estatus)
        { //obtiee todas las personas de la bd

            DataTable dtTemp = new DataTable();
            dtTemp.CaseSensitive = true;
            miSqlClass.conectar();

            if(Estatus == 2)
            {

                var a = miSqlClass.SqlConsulta("SELECT Id, CONCAT(Nombres, ' ',ApellidoP,' ',ApellidoM) Nombre, Telefono, Direccion, Estatus" +
                            "  FROM Personas ", ref dtTemp);
            }
            else
            {

                var a = miSqlClass.SqlConsulta("SELECT Id, CONCAT(Nombres, ' ',ApellidoP,' ',ApellidoM) Nombre, Telefono, Direccion, Estatus" +
                            "  FROM Personas WHERE Estauts ='"+ Estatus +"'", ref dtTemp);
            }

            List<mPersonas> miLista = new List<mPersonas>();

            miLista = (from rw in dtTemp.AsEnumerable()
                       select new mPersonas
                       {
                           Id = Convert.ToInt32(rw["Id"]),
                           Nombre = Convert.ToString(rw["Nombre"]),
                           Telefono = Convert.ToString(rw["Telefono"]),
                           Direccion = Convert.ToString(rw["Direccion"]),
                           Estatus = Convert.ToInt32(rw["Estatus"])
                       }).ToList();
            return miLista;
        }

        public List<mPersonas> obtenerPersonasBusqueda(string Busqueda)
        { //obtiee todas las personas de la bd

            DataTable dtTemp = new DataTable();
            dtTemp.CaseSensitive = true;
            miSqlClass.conectar();

            var a = miSqlClass.SqlConsulta("SELECT Id, CONCAT(Nombres, ' ',ApellidoP,' ',ApellidoM) Nombre, Telefono, Direccion, Estatus" +
                           "  FROM Personas" +
                           " WHERE Nombres LIKE '%" + Busqueda + "%'" +
                           " OR ApellidoP LIKE '%" + Busqueda + "%'" +
                           " OR ApellidoM LIKE '%" + Busqueda + "%'" , ref dtTemp);

            List<mPersonas> miLista = new List<mPersonas>();

            miLista = (from rw in dtTemp.AsEnumerable()
                       select new mPersonas
                       {
                           Id = Convert.ToInt32(rw["Id"]),
                           Nombre = Convert.ToString(rw["Nombre"]),
                           Telefono = Convert.ToString(rw["Telefono"]),
                           Direccion = Convert.ToString(rw["Direccion"]),
                           Estatus = Convert.ToInt32(rw["Estatus"])
                       }).ToList();
            return miLista;
        }


        public string insertPersona(mPersonas newPersona)
        {
            
            return ("");
        }

    }
}