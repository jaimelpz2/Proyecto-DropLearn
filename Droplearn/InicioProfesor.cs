using MySql.Data.MySqlClient;
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

namespace Droplearn
{
    public partial class InicioProfesor : Form
    {
        public int operacion = 0;//El que definira si es editar, crear o eliminar
        public int index;
        bool flag = false;
        bool flag2 = false; //PAra poder editar y añadir por separado en btn Ok click
        bool flagCurso = false;
        public int cur,cuentaCursos;
        string nombreProfesor;
        string nombre, clave, titleCurso;
        public string teacher;
        public string correo2, nombreCurso;
        Conexion conexion = new Conexion();
        ErrorProvider errorProvider = new ErrorProvider();
        public InicioProfesor()
        {
            InitializeComponent();
        }
        private void InicioProfesor_Load(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(100, 50, 50, 50);
            lblView.Visible = false;
            btnView.Visible = false;
            lblUser.Text = conexion.nombre(correo2);
            lblTest.Text = "";
         


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

            dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor,Escuela, cant_personas from cursos;");
            dgvCursos.Columns[0].HeaderText = "NAME";
            dgvCursos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[0].Width = 170;
            dgvCursos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCursos.Columns[1].HeaderText = "KEY";
            dgvCursos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[1].Width = 60;
            dgvCursos.Columns[2].HeaderText = "TEACHER";
            dgvCursos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[2].Width = 150;
            dgvCursos.Columns[3].HeaderText = "SCHOOL";
            dgvCursos.Columns[4].HeaderText = "LIMIT";
            dgvCursos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;





            flagCurso = false;
            txtTitle.Enabled = false;
            txtKey2.Enabled = false;
            txtLimit.Enabled = false;
            txtTeacher.Enabled = false;
            txtSchool.Enabled = false;
            btnOk.Visible = false;
            
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Boolean key = false, title = false, teacher = false, school = false, limit = false;
            if(operacion == 1)//Si la operacion es añadir o crear.
            {
                if (txtKey2.Text.Trim() == "")
                {
                    errorProvider.SetError(txtKey2, "Please write the course key");
                }
                else
                {
                    errorProvider.Clear();
                    key = true;
                }
                if (txtTitle.Text.Trim() == "")
                {
                    errorProvider.SetError(txtTitle, "Please write the course title");
                }
                else
                {
                    errorProvider.Clear();
                    title = true;
                }
                if (txtTeacher.Text.Trim() == "")
                {
                    errorProvider.SetError(txtTeacher, "Please write your name as teacher");
                }
                else
                {
                    errorProvider.Clear();
                    teacher = true;
                }
                if (txtSchool.Text.Trim() == "")
                {
                    errorProvider.SetError(txtSchool, "Please write the course school");
                }
                else
                {
                    errorProvider.Clear();
                    school = true;
                }
                if (txtLimit.Text.Trim() == "")
                {
                    errorProvider.SetError(txtLimit, "Please write the limit of people");
                }
                else
                {
                    errorProvider.Clear();
                    limit = true;
                }

                //add/añadir curso solo si los campos contienen texto
                if (key == true && title == true && teacher == true && school == true && limit == true)
                {
                    //conexion.ejecucion("Insert into cursos(Clave,Nombre,Profesor,Escuela,cant_personas) values ('" + txtKey.Text + "','" + txtTitle.Text + "','" + txtTeacher.Text + "','" + txtSchool.Text + "','" + txtLimit.Text + "')");
                    if (conexion.ejecucion("Insert into cursos(Clave,Nombre,Profesor,Escuela,cant_personas) values ('" + txtKey2.Text + "','" + txtTitle.Text + "','" + txtTeacher.Text + "','" + txtSchool.Text + "','" + txtLimit.Text + "')"))
                    {

                        if (conexion.validarCurso(nombre, correo2) == true)
                        {
                            MessageBox.Show("You have already created this course");
                        }//Si no existe el curso en la lista...
                        else if (conexion.validarCurso(nombre, correo2) == false)
                        {
                            cur++;
                            string query = "update perfil set Cursos='" + cur + "' where Correo='" + correo2 + "'";
                            conexion.ejecucion(query);
                            string query2 = "insert into listacursosid(Nombre,correoPerfil) values('" + txtTitle.Text.ToString() + "','" + correo2 + "')";
                            conexion.ejecucion(query2);
                            lblTest.Text = "Course *'" + txtTitle.Text + "'* successfully added";

                        }
                        MessageBox.Show("Course succesfully created");

                    }//tomar la cantidad de cursos de mi perfil teacher, y agregar el curso en lista cursos id

                  
                }
            }
            if (flag2 == true || operacion == 2)//Si la operacion es editar.
                {
                    MessageBox.Show("title:" + txtTitle.Text);
                    DialogResult resp = MessageBox.Show("You are going to modify a course. \n Are you sure?", "COURSE", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (resp == DialogResult.Yes)
                    {
                        string query = "update cursos set Clave='" + txtKey2.Text + "', Nombre='" + txtTitle.Text + "', Profesor='" + txtTeacher.Text + "', Escuela='" + txtSchool.Text + "', cant_personas='" + txtLimit.Text + "' where Nombre='"+nombre+"'";
                        if (conexion.ejecucion(query))
                        {
                            MessageBox.Show("Course successfully modified");
                        }
                    string query2 = "update listacursosid set Nombre='" + txtTitle.Text + "' where Nombre='" + nombre + "'";
                }
                }
                txtKey2.Text = "";
                txtTitle.Text = "";
                txtTeacher.Text = "";
                txtSchool.Text = "";
                txtLimit.Text = "";
                txtTitle.Enabled = false;
                txtKey2.Enabled = false;
                txtLimit.Enabled = false;
                txtTeacher.Enabled = false;
                txtSchool.Enabled = false;
                dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor,Escuela, cant_personas from cursos;");
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            operacion = 3;
            txtTitle.Enabled = true;
            txtKey2.Enabled = true;
            txtLimit.Enabled = true;
            txtTeacher.Enabled = true;
            txtSchool.Enabled = true;
            btnOk.Visible = true;
            //Si no eres dueño del curso o cursos...
            if (conexion.validarCurso(nombre, correo2) == false)
            {
                MessageBox.Show("You cannot do any operation with this course");
            }
            else
            {
                if (operacion == 3)//Si la operacion es eliminar.
                {
                    DialogResult resp = MessageBox.Show("You're going to delete a course. \n Are you sure?", "COURSE", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (resp == DialogResult.Yes)
                    {
                        string query = "delete from cursos where Nombre='" + nombre + "'";
                        if (conexion.ejecucion(query))
                        {
                            MessageBox.Show("Course deleted");
                        }
                        string query3 = "delete from listacursosid where Nombre='" + nombre + "'";
                        conexion.ejecucion(query3);
                        cur = cur - 1;
                        string query2 = "update perfil set Cursos='" + cur + "' where Correo='" + correo2 + "'";
                        conexion.ejecucion(query2);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            flag2 = true;
            operacion = 2;
            txtTitle.Enabled = true;
            txtKey2.Enabled = true;
            txtLimit.Enabled = true;
            txtTeacher.Enabled = true;
            txtSchool.Enabled = true;
            btnOk.Visible = true;
            
            //Si no eres dueño del curso o cursos...
            if (conexion.validarCurso(nombre, correo2) == false)
            {
                MessageBox.Show("You cannot do any operation with this course");
                dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor,Escuela, cant_personas from cursos;");
            }
            else
            {
                dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor,Escuela, cant_personas from cursos;");
                txtTitle.Text = dgvCursos.Rows[index].Cells[0].Value.ToString();
                txtKey2.Text = dgvCursos.Rows[index].Cells[1].Value.ToString();
                txtTeacher.Text = dgvCursos.Rows[index].Cells[2].Value.ToString();
                txtSchool.Text = dgvCursos.Rows[index].Cells[3].Value.ToString();
                txtLimit.Text = dgvCursos.Rows[index].Cells[4].Value.ToString();

            }
            


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            flag2 = false;
                operacion = 1; //añadir
                
                txtTitle.Enabled = true;
                txtTitle.Text = "";
                txtKey2.Enabled = true;
                txtKey2.Text = "";
                txtLimit.Enabled = true;
                txtLimit.Text = "";
                txtTeacher.Enabled = true;
                txtTeacher.Text = conexion.nombre(correo2);
                txtSchool.Enabled = true;
                txtSchool.Text = "";
                btnOk.Visible = true;
                dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor,Escuela, cant_personas from cursos;");


        }

        private void btnEvaluation_Click(object sender, EventArgs e)
        {
            Evaluaciones checar = new Evaluaciones();
            checar.cursos = cur;
            checar.correo = correo2;
            checar.teacher = teacher;
            checar.Show();

            this.Hide();
        }

        private void btnBoard_Click(object sender, EventArgs e)
        {
            Tablon tareas = new Tablon();
            tareas.cursos = cur;
            tareas.correo = correo2;
            tareas.teacher = teacher;
            tareas.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Perfil ver = new Perfil();
            ver.cursos = cur;
            ver.correo = correo2;
            ver.teacher = teacher;
            ver.Show();
            this.Hide();
        }

        private void btnCourses_Click(object sender, EventArgs e)//Your courses boton
        {
            dgvCursos.DataSource = conexion.cargarDatos("select Nombre from listacursosid where correoPerfil='" + correo2 + "';");
            dgvCursos.Columns[0].HeaderText = "YOUR COURSE'S NAME";
            dgvCursos.Columns[1].Visible = false;
            dgvCursos.Columns[2].Visible = false;
            dgvCursos.Columns[3].Visible = false;
            dgvCursos.Columns[4].Visible = false;
            dgvCursos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCursos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[0].Width = 531;
            lblView.Visible = true;
            btnView.Visible = true;
            lblTest.Text = "Here's the list of all the courses that you're in.";
            flagCurso = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor,Escuela, cant_personas from cursos;");
            txtKey2.Text = "";
            txtTitle.Text = "";
            txtTeacher.Text = "";
            txtSchool.Text = "";
            txtLimit.Text = "";
            txtTitle.Enabled = false;
            txtKey2.Enabled = false;
            txtLimit.Enabled = false;
            txtTeacher.Enabled = false;
            txtSchool.Enabled = false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            lblView.Visible = false;
            btnView.Visible = false;
            dgvCursos.Columns[1].Visible = true;
            dgvCursos.Columns[2].Visible = true;
            dgvCursos.Columns[3].Visible = true;
            dgvCursos.Columns[4].Visible = true;
            dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor,Escuela, cant_personas from cursos;");
            dgvCursos.Columns[0].HeaderText = "NAME";
            dgvCursos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[0].Width = 170;
            dgvCursos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCursos.Columns[1].HeaderText = "KEY";
            dgvCursos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[1].Width = 60;
            dgvCursos.Columns[2].HeaderText = "TEACHER";
            dgvCursos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCursos.Columns[2].Width = 150;
            dgvCursos.Columns[3].HeaderText = "SCHOOL";
            dgvCursos.Columns[4].HeaderText = "LIMIT";
            dgvCursos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lblAdd.Visible = true;
            btnAdd.Visible = true;
            flagCurso = false;
        }

        

        public bool validNum(Char c)
        {
            if (c >= '0' && c <= '9')
            {
                return true;
            }
            return false;
        }

        private void txtKey2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtLimit_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dgvCursos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label9.Visible = false;
            index = dgvCursos.CurrentRow.Index;
            nombre = dgvCursos.Rows[index].Cells[0].Value.ToString();
            clave = dgvCursos.Rows[index].Cells[1].Value.ToString();
            //contar la cantidad de personas que existen en el curso
            
            //Solo lectura del DataGridView
            dgvCursos.Rows[index].Cells[0].ReadOnly = true;
            dgvCursos.Rows[index].Cells[1].ReadOnly = true;
            dgvCursos.Rows[index].Cells[2].ReadOnly = true;
            dgvCursos.Rows[index].Cells[3].ReadOnly = true;
            dgvCursos.Rows[index].Cells[4].ReadOnly = true;
            if (index != 0)
            {
                flag = true;
            }
            nombreProfesor = conexion.nombre(correo2);
            //Mostrar comentarios y fecha para Coming Soon
            if (conexion.validarCurso(nombre, correo2) == true)
            {
                dgvComing.DataSource = conexion.cargarDatos2("select Publicacion,FechaProxima from tablon where idCursos='" + conexion.idCursosProfesor(nombreProfesor) + "'");
                dgvComing.Columns[0].HeaderText = "POST";
                dgvComing.Columns[1].HeaderText = "DATE LIMIT";
                dgvComing.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvComing.Columns[0].Width = 128;
                if (flagCurso == false)
                {
                    dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor,Escuela, cant_personas from cursos;");
                }
                else if (flagCurso == true)
                {
                    dgvCursos.DataSource = conexion.cargarDatos("select Nombre from listacursosid where correoPerfil='" + correo2 + "';");
                }


            }
            else if (conexion.validarCurso(nombre, correo2) == false)
            {
                MessageBox.Show("You don't own this course");
                dgvCursos.DataSource = conexion.cargarDatos("select Nombre,Clave,Profesor,Escuela, cant_personas from cursos;");
            }
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

        
    }
}
