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
    public partial class Book_set : UserControl
    {
        public Book_set()
        {
            InitializeComponent();
        }

        public Image ImageBook
        {
            get { return bunifuPictureBox2.Image; }

            set { bunifuPictureBox2.Image = value; }
        }

        public string NameBook
        {
            get { return label1.Text; }

            set { label1.Text = value; }
        }
        public string Avtor
        {
            get { return label2.Text; }

            set { label2.Text = value; }
        }

        public string TypeOfBook
        {
            get { return label3.Text; }

            set { label3.Text = value; }
        }

        public int RatingBook
        {
            get {   return bunifuRating1.Value; }

            set { bunifuRating1.Value = value; }
        }

        public string YearBook
        {
            get { return label4.Text; }

            set { label4.Text = value; }
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.Tag.ToString());

        }
    }
}
