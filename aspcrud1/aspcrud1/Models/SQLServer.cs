using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace aspcrud1.Models
{

    public class SQL
    {
        // *********************** [ VARIABLES SQL ] ***********************
        public static SqlConnection conSQL = new SqlConnection();
        public static SqlDataAdapter adaptadorSQL = new SqlDataAdapter();
        public static SqlCommand comandoSQL;
        public static SqlTransaction transaccionSQL;
        public static DataTable dtablaSQL = new DataTable();

        // FUNCION GLOBAL DE CONECCION
        public static bool coneccionSQL()
        {
            try
            {
                conSQL.ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
                return true;
            }
            catch
            {
                return false;
            }
        }

        // FUNCION QUE LLENA UN COMANDO SQL PARA PARSEAR EL RESULTADO SQL Y LLENA LAS CLASES (OBJETOS)
        public static SqlCommand commandoSQL(string query)
        {
            coneccionSQL();
            if (conSQL.State == ConnectionState.Closed || conSQL.State == ConnectionState.Connecting)
            {
                conSQL.Open();
            }
            var comando = new SqlCommand(query, conSQL);
            return comando;
        }

        // FUNCION QUE GENERA EL CONSTRUCTOR DE LA TRANSACCION (ABRE LA CONEXIÓN E INICIA EL COMANDO)
        public static bool comandoSQLTrans(string nomtrans)
        {
            coneccionSQL();
            if (conSQL.State == ConnectionState.Closed || conSQL.State == ConnectionState.Connecting)
            {
                conSQL.Open();
            }
            transaccionSQL = conSQL.BeginTransaction(nomtrans);
            return true;
        }
    }

    public class SqlClass
    {
        SqlConnection con;

        public bool conectar()
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;

            try
            {
                return true;
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                return false;
            }
            //return true;
        }

        // wsKioskoP.ServiceClient myKioskoP = new wsKioskoP.ServiceClient();
        SqlDataAdapter da;
        public respuestaCompuesta SqlConsulta(string consulta, ref DataTable dt)
        {
            conectar();
            respuestaCompuesta miRC = new respuestaCompuesta();
            miRC.error = false;
            miRC.query = consulta;
            dt.TableName = "temp";
            //return myKioskoP.SqlConsultaDT(consulta,ref dt);

            try
            {
                da = new SqlDataAdapter(consulta, con);
                miRC.RegistrosTabla = da.Fill(dt);
            }
            catch (Exception ex)
            {

                miRC.error = true;
                miRC.strError = ex.Message;
                //throw;
            }
            finally
            {
                con.Close();
            }
            return miRC;
        }

        public respuestaCompuesta SqlConsulta(string consulta)
        {
            //return myKioskoP.SqlConsultaSRT(consulta);
            respuestaCompuesta miRC = new respuestaCompuesta();
            miRC.query = consulta;
            miRC.error = false;

            int rowAffected = 0;
            conectar();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.CommandType = CommandType.Text;
                rowAffected = cmd.ExecuteNonQuery();
                miRC.columnasAfectadas = rowAffected;
                //return rowAffected;

            }
            catch (Exception ex)
            {
                miRC.error = true;
                miRC.strError = ex.Message;
                // return rowAffected;
                //throw;
            }
            finally
            {
                con.Close();
            }
            return miRC;
        }

        public int SqlConsulta(string[] consulta)
        {
            //return myKioskoP.SqlConsultaARR(consulta);
            int rowAffected = 0;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            for (int i = 0; i < consulta.Length; i++)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(consulta[i], con);
                    cmd.CommandType = CommandType.Text;
                    rowAffected += cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                    //hrow;
                }
            }

            // con.Close();
            //return rowAffected;

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            return rowAffected;
        }

        public int storeProc(String StoreProcedureName, List<parametro> parametros)
        {
            int rowAffected = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(StoreProcedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parametros.Count; i++)
                {
                    cmd.Parameters.AddWithValue(parametros[i].nombre, parametros[i].valor);
                }
                rowAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                //throw;
            }
            finally
            {
                con.Close();
            }
            return rowAffected;
        }

        public string procedurestring(String StoreProcedureName, List<parametro> parametros, ref string respuesta)
        {
            respuesta = string.Empty;
            try
            {
                conectar();
                con.Open();
                SqlCommand cmd = new SqlCommand(StoreProcedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parametros.Count; i++)
                {
                    cmd.Parameters.AddWithValue(parametros[i].nombre, parametros[i].valor);
                }
                respuesta = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            finally
            {
                con.Close();
            }
            return respuesta;
        }

        public string returnprocedurestring(string StoreProcedureName, List<parametro> parametros, ref string respuesta)
        {
            respuesta = string.Empty;
            SqlConnection sqlcon;
            SqlCommand sqlcmd;
            try
            {

                conectar();
                con.Open();
                sqlcmd = new SqlCommand(StoreProcedureName, con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                //sqlcmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = Username;
                for (int i = 0; i < parametros.Count; i++)
                {
                    sqlcmd.Parameters.AddWithValue(parametros[i].nombre, parametros[i].valor);
                }
                SqlParameter rolename = sqlcmd.Parameters.Add("@r", SqlDbType.VarChar);
                rolename.Direction = ParameterDirection.Output;
                //Session["Loginusername"] = Convert.ToString(rolename.Value);
                respuesta = rolename.Value.ToString();
                sqlcmd.Dispose();
                con.Close();

            }
            catch (SqlException sqlerr)
            {
                respuesta = sqlerr.ToString();
            }
            return respuesta;
        }
    }

    public class parametro
    {
        List<parametro> parametros = new List<parametro>();
        private String nombre_;
        public String nombre
        {
            get { return nombre_; }
            set { nombre_ = "@" + value; }
        }
        public String valor;
    }

    public class respuestaCompuesta
    {
        public int RegistrosTabla { get; set; }
        public int columnasAfectadas { get; set; }
        public bool error { get; set; }
        public string strError { get; set; }
        public string query { get; set; }
    }
}