using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
namespace db_projact
{
    public partial class Form1 : Form
    {
        OracleConnection con;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            Hide();
            login form = new login();
            form.ShowDialog();
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);
            
        }
        
        private void button_WOC2_Click(object sender, EventArgs e)
        {
            Hide();
            register form = new register();
            form.ShowDialog();
            Close();
        }

        private void button_WOC3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("thank you for your time around here :) ");
            Close();
        }
    }
}
