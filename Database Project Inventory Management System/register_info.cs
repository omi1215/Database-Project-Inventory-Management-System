using Oracle.ManagedDataAccess.Client;
using System;
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
    public partial class register_info : Form
    {
        OracleConnection con;
        public string username { get; set; }
        public register_info()
        {
            InitializeComponent();
        }

        private void register_info_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            con.Open();
            string str = username;
            OracleCommand command = con.CreateCommand();

            command.CommandText = "SELECT COUNT(*) FROM record WHERE username = :username";
            command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count == 0)
            {
                MessageBox.Show("Username already exists. Please choose a different one.");
            }
            else
            {
                command = con.CreateCommand();
                command.CommandText = "INSERT INTO customer(username,cname,accno,address)   VALUES (:username, :cname, :accno, :address)";
                command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                command.Parameters.Add("cname", OracleDbType.Varchar2).Value = customer_name.Text;
                command.Parameters.Add("accno", OracleDbType.Varchar2).Value = acc_no.Text;
                command.Parameters.Add("address", OracleDbType.Varchar2).Value = address_box.Text;
                command.CommandType = CommandType.Text;
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    Hide();
                    Form1 form = new Form1();
                    form.ShowDialog();
                    Close();
                }
            }

            con.Close();

           

          
        }
    }
}
