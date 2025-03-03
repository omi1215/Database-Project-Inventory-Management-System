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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace db_projact
{
    public partial class register : Form
    {
        OracleConnection con;
        public register()
        {
            InitializeComponent();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            con.Open();
            OracleCommand command = con.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM record WHERE username = :username";
            command.Parameters.Add("username", OracleDbType.Varchar2).Value = username_input.Text;
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count > 0)
            {
                MessageBox.Show("Username already exists. Please choose a different one.");
            }
            else
            {
                command = con.CreateCommand();
                command.CommandText = "INSERT INTO record VALUES (:username, :password, :position)";
                command.Parameters.Add("username", OracleDbType.Varchar2).Value = username_input.Text;
                command.Parameters.Add("password", OracleDbType.Varchar2).Value = password_input.Text;
                command.Parameters.Add("position", OracleDbType.Varchar2).Value = "customer";
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    Hide();
                    register_info form = new register_info();
                    form.username = username_input.Text;
                    form.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("An error occurred while registering. Please try again later.");
                }
            }

            con.Close();
        }


        private void button_WOC2_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            Close();
        }

        private void register_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void username_input_TextChanged(object sender, EventArgs e)
        {
           
        
        }
    }
}
