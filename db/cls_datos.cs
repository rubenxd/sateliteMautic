using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SateliteMautic.db
{
   public class cls_datos
    {
        //instancias
        cls_conexion cn = new cls_conexion();

        //Variables
        DataTable v_DT;
        SqlDataAdapter v_SDA;
        SqlCommand v_SC;
        string v_query = "";
        int v_contador = 0;
        bool v_ok = true;

        //Metodos
        public DataTable mtd_consultar(string query)
        {
            v_DT = null;
            v_query = query;

            if (cn.Cadenacn.State == ConnectionState.Open)
            {
                cn.Cadenacn.Close();
            }

            try
            {
                cn.Cadenacn.Open();
                v_SDA = new SqlDataAdapter(v_query, cn.Cadenacn);
                v_DT = new DataTable();
                v_SDA.Fill(v_DT);
                cn.Cadenacn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                v_SDA.Dispose();
            }

            return v_DT;
        }
        public Boolean mtd_registrar(SqlParameter[] Parametros, string query)
        {
            v_query = query;
            cn.Cadenacn.Open();
            v_SC = new SqlCommand(v_query, cn.Cadenacn);
            v_contador = 0;

            while (v_contador < Parametros.Length)
            {
                //// Creando los parámetros necesarios
                v_SC.Parameters.Add(Parametros[v_contador].ParameterName, Parametros[v_contador].SqlDbType);

                //// Asignando los valores a los atributos
                v_SC.Parameters[Parametros[v_contador].ParameterName].Value = Parametros[v_contador].Value;

                v_contador++;
            }

            try
            {
                v_SC.ExecuteNonQuery();
                cn.Cadenacn.Close();
                v_ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar registrar: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v_ok = false;
            }

            return v_ok;
        }
        public Boolean mtd_editar(SqlParameter[] Parametros, string query)
        {
            if (cn.Cadenacn.State == ConnectionState.Open)
            {
                cn.Cadenacn.Close();
            }

            v_query = query;
            cn.Cadenacn.Open();
            v_SC = new SqlCommand(v_query, cn.Cadenacn);
            v_contador = 0;

            if (Parametros != null)
            {
                while (v_contador < Parametros.Length)
                {
                    //// Creando los parámetros necesarios
                    v_SC.Parameters.Add(Parametros[v_contador].ParameterName, Parametros[v_contador].SqlDbType);

                    //// Asignando los valores a los atributos
                    v_SC.Parameters[Parametros[v_contador].ParameterName].Value = Parametros[v_contador].Value;

                    v_contador++;
                }
            }

            try
            {
                v_SC.ExecuteNonQuery();
                cn.Cadenacn.Close();
                v_ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar modificar: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v_ok = false;
            }

            return v_ok;
        }
        public Boolean mtd_ejecutar(string query)
        {
            v_query = query;

            try
            {
                cn.Cadenacn.Open();
                v_SC = new SqlCommand(v_query, cn.Cadenacn);
                v_SC.ExecuteNonQuery();
                cn.Cadenacn.Close();
                v_ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar EJECUTAR: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v_ok = false;
            }

            return v_ok;
        }
        public Boolean mtd_eliminar(string query)
        {
            v_query = query;

            try
            {
                cn.Cadenacn.Open();
                v_SC = new SqlCommand(v_query, cn.Cadenacn);
                v_SC.ExecuteNonQuery();
                cn.Cadenacn.Close();
                v_ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Eliminar: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                v_ok = false;
            }

            return v_ok;
        }
    }
}
