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
    public partial class Main_admin : Form
    {
        List<Book_set> book_control = new List<Book_set>();
        List<LinkLabel> Link_autor = new List<LinkLabel>();
        List<LinkLabel> Link_zaner = new List<LinkLabel>();
        string path = @"Base\bases.db"; //Путь к файлу БД
        public static List<Data> combo_avtor = new List<Data>();
        public static List<Data> combo_zanar = new List<Data>();
        DataTable dt_avtor = new DataTable();
        DataTable dt_zanar = new DataTable();

        [Obsolete]
        public Main_admin(string s)
        {
            InitializeComponent();
            label1.Text = s;
            Load_comboBox();
            Load_book();
            Load_autor();
            Load_gener();
            Book_add_DB plus = new Book_add_DB();
            plus.Parent = this.flowLayoutPanel1;
         }

        private void Load_comboBox()
        {

            using (var connection = new SqliteConnection(@"Data Source = " + path))
            {
                connection.Open();
                dt_avtor = new DataTable();
                dt_zanar = new DataTable();
                SqliteCommand cmd = new SqliteCommand("SELECT * FROM Author", connection);
                SqliteDataReader dr = cmd.ExecuteReader();
                dt_avtor.Load(dr);
                cmd = new SqliteCommand("SELECT * FROM Genre", connection);
                dr = cmd.ExecuteReader();
                dt_zanar.Load(dr);

                foreach (DataRow row in dt_avtor.Rows) //заносим в список авторов
                {
                    combo_avtor.Add(new Data(int.Parse(row[0].ToString()), row[1].ToString()));
                }

                foreach (DataRow row in dt_zanar.Rows) //заносим в список авторов
                {
                    combo_zanar.Add(new Data(int.Parse(row[0].ToString()), row[1].ToString()));
                }
            }
        }

        private void Load_book()
        {
            using (var connection = new SqliteConnection(@"Data Source = " + path))
            {
                connection.Open();
                dt_avtor = new DataTable();
                dt_zanar = new DataTable();
                SqliteCommand cmd = new SqliteCommand("SELECT Book.ID,Book.Name, Author.Name_author, Genre.Name, Book.Image_Book, Book.Year FROM Book INNER JOIN Author ON Author.ID = Book.Author INNER JOIN Genre ON Genre.ID = Book.Genre", connection);
                SqliteDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows) // если есть данные
                {
                    while (dr.Read())   // построчно считываем данные
                    {
                        book_control.Add(new Book_set());
                        book_control[0].Parent = this.flowLayoutPanel1;
                        book_control[0].Tag = int.Parse(dr[0].ToString());
                        book_control[0].NameBook = dr[1].ToString();
                        book_control[0].Avtor = dr[2].ToString();
                        book_control[0].TypeOfBook = dr[3].ToString();
                        byte[] data = (byte[])dr.GetValue(4);
                        MemoryStream ms = new MemoryStream(data);
                        Image returnImage = Image.FromStream(ms);
                        book_control[0].ImageBook = returnImage;
                        book_control[0].YearBook = dr[5].ToString();
                        book_control[0].RatingBook = 5;
                        flowLayoutPanel1.Controls.SetChildIndex(flowLayoutPanel1.Controls[0], 1);



                    }
                }
         }
    }

        private void Load_autor()
        {
            Link_autor.Clear();
            foreach (DataRow row in dt_avtor.Rows) //заносим в список авторов
            {



                combo_avtor.Add(new Data(int.Parse(row[0].ToString()), row[1].ToString()));
            }
        }
        private void Load_gener()
        {
            //формируем список из ссылкок
        }
        private void Main_admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(((Control)sender).Text);        
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("Читательские");
        }

        private void Main_admin_Load(object sender, EventArgs e)
        {
            // получить базу данных книг и заполнить


        }

        private void add_book1_Click(object sender, EventArgs e)
        {
            
        }
    }



    public class Data
    {
        public string Name { set; get; }
        public int id { set; get; }
        public Data(int id, string Name)
        {
            this.Name = Name;
            this.id = id;
        }
    }
}
