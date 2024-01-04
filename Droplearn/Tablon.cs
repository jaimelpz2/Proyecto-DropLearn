using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Droplearn
{
    public partial class Tablon : Form
    {
        InicioEstudiante inicio = new InicioEstudiante();
        public int cursos;
        public string correo;
        public string teacher, student;
        public Tablon()
        {
            InitializeComponent();
        }

        private void Tablon_Load(object sender, EventArgs e)
        {
            MessageBox.Show("correo:" + correo);
            panel1.BackColor = Color.FromArgb(100, 50, 50, 50);
            panel2.BackColor = Color.FromArgb(100, 50, 50, 50);
            panel3.BackColor = Color.FromArgb(100, 50, 50, 50);
            MessageBox.Show("maestro o estu: " + teacher);
            MessageBox.Show("maestro o estu: " + student);
        }

        private void button1_Click(object sender, EventArgs e)// Botón Inicio
        {
            if (teacher == "Teacher")
            {
                InicioProfesor volverProfe = new InicioProfesor();
                volverProfe.cur = cursos;
                volverProfe.correo2 = correo;
                volverProfe.teacher = teacher;
                volverProfe.Show();
            }
            else if (student == "Student")
            {
                InicioEstudiante volver4 = new InicioEstudiante();
                volver4.cur = cursos;
                volver4.correo2 = correo;
                volver4.student = student;
                volver4.Show();
            }
            

            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            

            Graphics mgraficos = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(96, 150, 50), 1);

            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(0, 139, 139), Color.FromArgb(220, 20, 60), LinearGradientMode.ForwardDiagonal);
            mgraficos.FillRectangle(lgb, area);
            mgraficos.DrawRectangle(pen, area);
        }

        

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mgraficos = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(96, 150, 50), 1);

            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(220, 20, 60), Color.FromArgb(0, 139, 139), LinearGradientMode.BackwardDiagonal);
            mgraficos.FillRectangle(lgb, area);
            mgraficos.DrawRectangle(pen, area);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mgraficos = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(96, 150, 50), 1);

            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(220, 20, 60), Color.FromArgb(0, 139, 139), LinearGradientMode.ForwardDiagonal);
            mgraficos.FillRectangle(lgb, area);
            mgraficos.DrawRectangle(pen, area);
        }

        
    }
}
