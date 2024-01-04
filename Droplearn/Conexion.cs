using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Droplearn {
    
    

    class Conexion
    {
        public string correo;
        public string correo3;
        public string id;
        public int flag = 0;
        public MySqlConnection conex;
        public String strConexion = "server=127.0.0.1; user id=root; password=123; database=droplearn; SslMode=none";
        public DataTable dt = new DataTable();
        public DataTable dt2 = new DataTable();

        

        public bool abrir()
        {
            try
            {
                conex = new MySqlConnection(strConexion);
                conex.Open();
                dt.Clear();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo hacer conexion con la BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

       public  MySqlConnection conectando()//refres perfil
        {
            
            try
            {
                conex = new MySqlConnection(strConexion);
               
                return conex;
            }catch(MySqlException ex)
            {
                Console.Write("Error: " + ex.Message);
                return null;
            }
            
        }

        public DataTable cargarDatos(String query)
        {
            try
            {

                abrir();
                MySqlDataAdapter daDatos = new MySqlDataAdapter(query, conex);
                daDatos.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo hacer conexion con la BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dt;
            }
        }
        public DataTable cargarDatos2(String query)
        {
            try
            {            
                abrir();
                dt2.Clear();
                MySqlDataAdapter daDatos = new MySqlDataAdapter(query, conex);
                daDatos.Fill(dt2);
                return dt2;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo hacer conexion con la BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dt2;
            }
        }


        public bool ejecucion(String query)
        {
            try
            {
                
                abrir();
                MySqlCommand cmdQuery = new MySqlCommand(query, conex);
                cmdQuery.ExecuteNonQuery();
                conex.Close();
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo hacer conexion con la BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public string perfil(String query)
        {
            string x;
            abrir();
            MySqlCommand cmd = new MySqlCommand(query, conex);
            x = cmd.ExecuteScalar().ToString();
            conex.Close();
            return x;
        }

       

        public void logear(String correo, String contraseña)
        {
            
            try
            {
                abrir();
                MySqlCommand cmd = new MySqlCommand("SELECT Nombre, TipodeUsuario FROM registro WHERE Correo = @correo AND Contraseña = @contraseña",conex);
                cmd.Parameters.AddWithValue("correo", correo);
                cmd.Parameters.AddWithValue("contraseña", contraseña);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                flag = 0;

                
                if (dt.Rows.Count == 1)
                {
                    if(dt.Rows[0][1].ToString() == "Student")
                    {
                        InicioEstudiante inicio = new InicioEstudiante();
                        flag = 1;
                        string query = "select Cursos from perfil where Correo='" + correo + "'";
                        MySqlCommand cmd2 = new MySqlCommand(query, conex);
                        int cursillo = Convert.ToInt32(cmd2.ExecuteScalar());
                        inicio.cur = cursillo;
                        inicio.correo2 = correo;
                        inicio.student = dt.Rows[0][1].ToString();
                        inicio.Show();
                    }
                    else if(dt.Rows[0][1].ToString() == "Teacher")
                    {
                        InicioProfesor inicioProfesor = new InicioProfesor();
                        flag = 1;
                        string query = "select Cursos from perfil where Correo='" + correo + "'";
                        MySqlCommand cmd2 = new MySqlCommand(query, conex);
                        int cursillo = Convert.ToInt32(cmd2.ExecuteScalar());
                        inicioProfesor.cur = cursillo;
                        inicioProfesor.correo2 = correo;
                        inicioProfesor.teacher = dt.Rows[0][1].ToString();
                        inicioProfesor.Show();
                    }
                }
                else
                {
                    flag = 0;
                    MessageBox.Show("Usuario y/o Contraseña Incorrecta");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool validarCurso(String c, String correo)// c = nombre del curso
        {
                abrir();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM listacursosid WHERE Nombre = @c AND correoPerfil= @correo", conex);
                cmd.Parameters.AddWithValue("c", c);
                cmd.Parameters.AddWithValue("correo", correo);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                flag = 0;
                if (dt.Rows.Count >= 1)
                {
                //MessageBox.Show("Si existe");
                    return true;
                }
            //MessageBox.Show("No existe");
            return false;
            
        }


        public int idReg()
        {
            abrir();
            string query = "select MAX(idRegistro) AS LASTID from registro";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            int lastId = Convert.ToInt32(cmd.ExecuteScalar());
            return lastId;
        }
        public int cuentaCursos(String nombreCurso)//Contar hasta el limite de personas registradas en un curso
        {
            abrir();
            string query = "select count(*) from listacursosid where Nombre='"+nombreCurso+"'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count;
        }

        public int cantPersonas(String nombreCurso)//Para obtener el limite de cantidad de personas de un curso y definir el stop de insert.
        {
            abrir();
            string query = "select cant_personas from cursos where Nombre='" + nombreCurso + "'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            int cantP = Convert.ToInt32(cmd.ExecuteScalar());
            return cantP;
        }
        public int idCursos(String nombreCurso)
        {
            abrir();
            string query = "select idCursos from cursos where Nombre='"+nombreCurso+"'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            int idCur = Convert.ToInt32(cmd.ExecuteScalar());
            return idCur;
        }
        public int idCursosProfesor(String nombreProfesor)
        {
            abrir();
            string query = "select idCursos from cursos where Profesor='" + nombreProfesor + "'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            int idCur = Convert.ToInt32(cmd.ExecuteScalar());
            return idCur;
        }

        public string nombre(string correo)//Nombre de usuario
        {
            abrir();
            string query = "select Nombre from registro where Correo='" + correo + "'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            string name = Convert.ToString(cmd.ExecuteScalar());
            return name;
        }
        public string Escolaridad(string correo)
        {
            abrir();
            string query = "select Escolaridad from perfil where Correo='" + correo + "'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            string esco = Convert.ToString(cmd.ExecuteScalar());
            return esco;
        }

        public string Cursos(string correo)
        {
            abrir();
            string query = "select Cursos from perfil where Correo='" + correo + "'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            string esco = Convert.ToString(cmd.ExecuteScalar());
            return esco;
        }

        public string NiveldeEstudios(string correo)
        {
            abrir();
            string query = "select NiveldeEstudios from perfil where Correo='" + correo + "'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            string esco = Convert.ToString(cmd.ExecuteScalar());
            return esco;
        }

        public string Edad(string correo)
        {
            abrir();
            string query = "select Edad from perfil where Correo='" + correo + "'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            string esco = Convert.ToString(cmd.ExecuteScalar());
            return esco;
        }


        public string Imagen(string correo)
        {
            abrir();
            string query = "select imagen from perfil where Correo='" + correo + "'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            string esco = Convert.ToString(cmd.ExecuteScalar());
            return esco;
        }



        public string idPerfil(string correo)
        {
            abrir();
            string query = "select idPerfil from perfil where Correo='" + correo + "'";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            string esco = Convert.ToString(cmd.ExecuteScalar());
            
            return esco;
        }

    }
}
