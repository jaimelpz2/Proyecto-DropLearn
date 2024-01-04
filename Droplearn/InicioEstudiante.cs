using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Droplearn
{
    public partial class InicioEstudiante : Form
    {
        bool flag = false;
        bool flagCurso = false;
        public string student;
        public int cur,cuentaCursos, limite;
        string nombre, clave;
        public string correo2,nombreCurso;
        Conexion conexion = new Conexion();
        public InicioEstudiante()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(100, 50, 50, 50);
            lblView.Visible = false;
            btnView.Visible = false;
            lblUser.Text = conexion.nombre(correo2);
            lblCur.Text = "";
           


            string perfil = conexion.idPerfil(correo2);
            string query2 = "select imagen from perfil where idPerfil='" + perfil + "'";

            MySqlConnection conexionBD = conexion.conectando();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(query2, conexionBD);
                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    if (!Convert.IsDBNull(reader["imagen"]))
                    {
                        MemoryStream ms2 = new MemoryStream((byte[])reader["imagen"]);
                        Bitmap bm = new Bitmap(ms2);
                        pictureBox1.Image = bm;
                    }


                }
                else
                {
                    MessageBox.Show("it doesn't exits register ");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar" + ex.Message);
            }

            dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor from cursos;");
            dgvCursos.Columns[0].HeaderText = "NAME";
            dgvCursos.Columns[1].HeaderText = "KEY";
            dgvCursos.Columns[2].HeaderText = "TEACHER";
            dgvCursos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[0].Width = 190;
            dgvCursos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[1].Width = 60;
            dgvCursos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            flagCurso = false;



        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mgraficos = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(96, 150, 50), 1);

            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(220, 20, 60), Color.FromArgb(0, 139, 139), LinearGradientMode.Vertical);
            mgraficos.FillRectangle(lgb, area);
            mgraficos.DrawRectangle(pen, area);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mgraficos = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(96, 150, 50), 1);

            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(220, 20, 60), Color.FromArgb(0, 139, 139), LinearGradientMode.ForwardDiagonal);
            mgraficos.FillRectangle(lgb, area);
            mgraficos.DrawRectangle(pen, area);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mgraficos = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(96, 150, 50), 1);

            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(220, 20, 60), Color.FromArgb(0, 139, 139), LinearGradientMode.Horizontal);
            mgraficos.FillRectangle(lgb, area);
            mgraficos.DrawRectangle(pen, area);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mgraficos = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(96, 150, 50), 1);

            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(220, 20, 60), Color.FromArgb(0, 139, 139), LinearGradientMode.Vertical);
            mgraficos.FillRectangle(lgb, area);
            mgraficos.DrawRectangle(pen, area);
        }

        private void pictureBox1_Click(object sender, EventArgs e)

        {
           

            Perfil ver = new Perfil();
            ver.cursos = cur;
            ver.correo = correo2;
            ver.student = student;
            ver.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//Clicks en el DataGrid
        {
            label8.Visible = false;
            int index = dgvCursos.CurrentRow.Index;
            nombre = dgvCursos.Rows[index].Cells[0].Value.ToString();
            clave = dgvCursos.Rows[index].Cells[1].Value.ToString();
            txtKey.Text = clave;
            //Solo lectura del DataGridView
            dgvCursos.Rows[index].Cells[0].ReadOnly = true;
            dgvCursos.Rows[index].Cells[1].ReadOnly = true;
            dgvCursos.Rows[index].Cells[2].ReadOnly = true;
            if (index != 0)
            {
                flag = true;
            }
            //Mostrar comentarios y fecha para Coming Soon
            if (conexion.validarCurso(nombre, correo2) == true)
            {
                dgvComing.DataSource = conexion.cargarDatos2("select Publicacion,FechaProxima from tablon where idCursos='" + conexion.idCursos(nombre) + "'");
                dgvComing.Columns[0].HeaderText = "POST";
                dgvComing.Columns[1].HeaderText = "DATE LIMIT";
                dgvComing.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvComing.Columns[0].Width = 128;
                if(flagCurso == false)
                { 
                    dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor from cursos;"); 
                }else if(flagCurso == true)
                {
                    dgvCursos.DataSource = conexion.cargarDatos("select Nombre from listacursosid where correoPerfil='" + correo2 + "';");
                }
                
                
            }
            else if(conexion.validarCurso(nombre, correo2) == false)
            {
                MessageBox.Show("You dont have this course yet");
                dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor from cursos;");
            }
            cuentaCursos = conexion.cuentaCursos(nombre);
            //MessageBox.Show("Cuentacursos:" + cuentaCursos);
            limite = conexion.cantPersonas(nombre);
            //MessageBox.Show("Limite:" + limite);
            dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor from cursos;");





        }

        private void button1_Click(object sender, EventArgs e)//Boton Board
        {
            Tablon tareas = new Tablon();
            tareas.cursos = cur;
            tareas.correo = correo2;
            tareas.student = student; 
            
            tareas.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)//Boton Evaluation
        {
            Evaluaciones checar = new Evaluaciones();
            checar.cursos = cur;
            checar.student = student;
            checar.correo = correo2;
            checar.Show();

            this.Hide();
        }

        private void btnJoin_Click(object sender, EventArgs e) //Botón Join
        {
            if(txtKey.Text.Length == 0)
            {
                MessageBox.Show("Please write the course's key");
            }
            string query;
            if (txtKey.Text.ToString() == clave)
            {//Si selecciona un campo del DataGridView...
                if (flag == true)//Bandera en true significa que se seleccionó algo
                {
                    //Si aun hay limite
                    cuentaCursos = conexion.cuentaCursos(nombre);
                    //MessageBox.Show("Cuentacursos:" + cuentaCursos);
                    limite = conexion.cantPersonas(nombre);
                    //MessageBox.Show("Limite:" + limite);
                    if (cuentaCursos != limite)
                    {
                        //Si no existe el curso en la lista...
                        if (conexion.validarCurso(nombre, correo2) == true)
                        {
                            MessageBox.Show("You have already added this course");
                        }
                        else if (conexion.validarCurso(nombre, correo2) == false)
                        {
                            cur++;
                            query = "update perfil set Cursos='" + cur + "' where Correo='" + correo2 + "'";
                            if (conexion.ejecucion(query))
                            {
                                MessageBox.Show("Course added to your profile");

                            }
                            string query2 = "insert into listacursosid(Nombre,correoPerfil) values('" + nombre + "','" + correo2 + "')";
                            conexion.ejecucion(query2);
                            string query3 = "insert into evaluaciones(Nombre,Correo,TotaldeActividades,Progreso,idCursos) values('" + conexion.nombre(correo2) + "','" + correo2 + "',0,0,'" + conexion.idCursos(nombre) + "')";
                            conexion.ejecucion(query3);
                            lblCur.Text = "Course *'" + nombre + "'* successfully added";
                            

                        }
                    }
                    else
                    {
                        MessageBox.Show("This course has already reached the limit of people");
                    }
                }
                dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor from cursos;");
                
            }
            


        }
        
        private void txtKey_KeyPress(object sender, KeyPressEventArgs e)//Validar sólo números en el text de Clave
        {
            if (validNum(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblCur_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)//Botón de 'Your courses'
        {
            dgvCursos.DataSource = conexion.cargarDatos("select Nombre from listacursosid where correoPerfil='"+correo2+"';");
            dgvCursos.Columns[0].HeaderText = "YOUR COURSE'S NAME";
            dgvCursos.Columns[1].Visible = false;
            dgvCursos.Columns[2].Visible = false;
            dgvCursos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCursos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[0].Width = 400;
            lblView.Visible = true;
            btnView.Visible = true;
            lblJoin.Visible = false;
            btnJoin.Visible = false;
            lblKey.Visible = false;
            txtKey.Visible = false;
            label7.Text = "Here's the list of all the courses that you're in.";
            flagCurso = true;


        }

        private void btnRefresh_Click(object sender, EventArgs e) //Boton Refresh
        {
            dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor from cursos;");
            txtKey.Text = "";
            
        }

        private void btnView_Click(object sender, EventArgs e)//Botón First view
        {
            lblView.Visible = false;
            btnView.Visible = false;
            dgvCursos.Columns[1].Visible = true;
            dgvCursos.Columns[2].Visible = true;
            dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor from cursos;");
            dgvCursos.Columns[0].HeaderText = "NAME";
            dgvCursos.Columns[1].HeaderText = "KEY";
            dgvCursos.Columns[2].HeaderText = "TEACHER";
            dgvCursos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[0].Width = 190;
            dgvCursos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[1].Width = 60;     
            dgvCursos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            lblJoin.Visible = true;
            btnJoin.Visible = true;
            lblKey.Visible = true;
            txtKey.Visible = true;
            flagCurso = false;
        }

        public bool validNum(Char c)
        {
            if(c >='0' && c <= '9')
            {
                return true;
            }
            return false;
        }
    }
}
