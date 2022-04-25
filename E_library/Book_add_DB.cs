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
    public partial class Book_add_DB : UserControl
    {
        
        public Book_add_DB()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Add_new_book book = new Add_new_book();
            book.Show();
        }
    }
}
