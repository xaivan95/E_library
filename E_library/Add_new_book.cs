using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_library
{
    public partial class Add_new_book : Form
    {
        public Add_new_book()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            bunifuDropdown1.DataSource = Main_admin.combo_avtor;
            bunifuDropdown1.DisplayMember = "Name";
            bunifuDropdown1.ValueMember = "id";
            bunifuDropdown2.DataSource = Main_admin.combo_zanar;
            bunifuDropdown2.DisplayMember = "Name";
            bunifuDropdown2.ValueMember = "id";

        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            bunifuTextBox3.Text = filename;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        string path = @"Base\bases.db"; //Путь к файлу БД
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string sqlExpression = "INSERT INTO Book (Name, Author, Genre, Image_Book, Year) VALUES (@name, @aut, @genr, @image, @year)";
            using (var connection = new SqliteConnection(@"Data Source = " + path))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                // создаем параметр для имени
                SqliteParameter nameParam = new SqliteParameter("@name", bunifuTextBox1.Text);
                // добавляем параметр к команде
                command.Parameters.Add(nameParam);
                // создаем параметр
                SqliteParameter yearParam = new SqliteParameter("@year", bunifuTextBox2.Text);
                // добавляем параметр к команде
                command.Parameters.Add(yearParam);
                // создаем параметр
                SqliteParameter autParam = new SqliteParameter("@aut", bunifuDropdown1.SelectedValue);
                // добавляем параметр к команде
                command.Parameters.Add(autParam);
                // создаем параметр
                SqliteParameter genParam = new SqliteParameter("@genr", bunifuDropdown2.SelectedValue);
                // добавляем параметр к команде
                command.Parameters.Add(genParam);
                byte[] imageData;
                using (FileStream fs = new FileStream(bunifuTextBox3.Text, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                // создаем параметр
                SqliteParameter imgParam = new SqliteParameter("@image", imageData);
                // добавляем параметр к команде
                command.Parameters.Add(imgParam);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Книга добавлена.");
                this.Hide();
            }
        }
    }
}
