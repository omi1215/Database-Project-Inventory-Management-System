﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_projact
{
    public partial class loading : Form
    {
        public loading()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            Close();    
           
        }

        private void loading_Load(object sender, EventArgs e)
        {

        }
    }
}
