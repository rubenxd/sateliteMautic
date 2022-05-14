using SateliteMautic.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SateliteMautic.Model
{
    public class Contacto
    {
        //instancias
        cls_conexion cls_cn = new cls_conexion();
        cls_datos cls_datos = new cls_datos();
        string v_query = "";
        DataTable v_dt = new DataTable();
        SqlParameter[] Parametros;
        bool v_ok;

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Celular { get; set; }
        public string email { get; set; }
        public int Integrado { get; set; }
        public DateTime FechaRegistro { get; set; }

        private void mtd_asignaParametros()
        {
            Parametros = new SqlParameter[7];

            Parametros[0] = new SqlParameter();
            Parametros[0].ParameterName = "@Id";
            Parametros[0].SqlDbType = SqlDbType.VarChar;
            Parametros[0].SqlValue = Id;

            Parametros[1] = new SqlParameter();
            Parametros[1].ParameterName = "@Nombre";
            Parametros[1].SqlDbType = SqlDbType.VarChar;
            Parametros[1].SqlValue = Nombre;

            Parametros[2] = new SqlParameter();
            Parametros[2].ParameterName = "@Apellido";
            Parametros[2].SqlDbType = SqlDbType.VarChar;
            Parametros[2].SqlValue = Apellido;

            Parametros[3] = new SqlParameter();
            Parametros[3].ParameterName = "@Celular";
            Parametros[3].SqlDbType = SqlDbType.VarChar;
            Parametros[3].SqlValue = Celular;

            Parametros[4] = new SqlParameter();
            Parametros[4].ParameterName = "@email";
            Parametros[4].SqlDbType = SqlDbType.VarChar;
            Parametros[4].SqlValue = email;

            Parametros[5] = new SqlParameter();
            Parametros[5].ParameterName = "@Integrado";
            Parametros[5].SqlDbType = SqlDbType.Int;
            Parametros[5].SqlValue = Integrado;

            Parametros[6] = new SqlParameter();
            Parametros[6].ParameterName = "@FechaRegistro";
            Parametros[6].SqlDbType = SqlDbType.DateTime;
            Parametros[6].SqlValue = FechaRegistro;

        }
        public Boolean mtd_registrar()
        {
            v_query = " INSERT INTO Contact (Id,Nombre,Apellido,Celular,email,Integrado,FechaRegistro)" +
                      " VALUES (@Id,@Nombre,@Apellido,@Celular,@email,@Integrado,@FechaRegistro)";

            mtd_asignaParametros();
            v_ok = cls_datos.mtd_registrar(Parametros, v_query);
 
            return v_ok;
        }
    }
}
