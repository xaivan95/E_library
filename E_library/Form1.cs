using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bunifuTextBox2.UseSystemPasswordChar = true;
            bunifuTextBox2.PasswordChar = '*';
        }

        private bool isMousePress = false;
        private Point _clickPoint;
        private Point _formStartPoint;

        [Obsolete]
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string path = @"Base\bases.db"; //Путь к файлу БД
            using (var connection = new SqliteConnection(@"Data Source = " + path))
            {
                connection.Open();

                string s = bunifuTextBox1.Text.ToLower();
               // s = s.Replace(" ", string.Empty);
                SqliteCommand cmd = new SqliteCommand("select * from User where login = '" + s + "' and password ='" + bunifuTextBox2.Text + "'", connection);
                SqliteDataReader dr = cmd.ExecuteReader();
                string name = "";
                int count = 0;
                while (dr.Read())
                {
                    count = count + 1;
                    name = dr[1].ToString();
                }
                if (count == 1)
                {
                    Main_admin fr = new Main_admin(name);
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Логин или пароль неверны!");
                }
            }
        }

        private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            isMousePress = true;
            _clickPoint = Cursor.Position;
            _formStartPoint = Location;
        }

        private void bunifuGradientPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMousePress)
            {
                var cursorOffsetPoint = new Point( //считаем смещение курсора от старта
                    Cursor.Position.X - _clickPoint.X,
                    Cursor.Position.Y - _clickPoint.Y);

                Location = new Point( //смещаем форму от начальной позиции в соответствии со смещением курсора
                    _formStartPoint.X + cursorOffsetPoint.X,
                    _formStartPoint.Y + cursorOffsetPoint.Y);
            }
        }

        private void bunifuGradientPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePress = false;
            _clickPoint = Point.Empty;
        }

        private void bunifuTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                bunifuButton1_Click(sender, e);
            }
        }
    }
}
