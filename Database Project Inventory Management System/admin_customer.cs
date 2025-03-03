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
    public partial class admin_customer : Form
    {
        OracleConnection con;
        public int check { get; set; }
        public string username { get; set; }
        public admin_customer()
        {
            InitializeComponent();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {

            con.Open();
            string str = username;
            OracleCommand command = con.CreateCommand();
            if (check == 1)
            {
                command = con.CreateCommand();
                command.CommandText = "INSERT INTO customer(username,cname,accno,address)   VALUES (:username, :cname, :accno, :address)";
                command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                command.Parameters.Add("cname", OracleDbType.Varchar2).Value = customer_name.Text;
                command.Parameters.Add("accno", OracleDbType.Varchar2).Value = acc_no.Text;
                command.Parameters.Add("address", OracleDbType.Varchar2).Value = address_box.Text;
                command.CommandType = CommandType.Text;
            }
            else if (check == 0)
            {
                command.CommandText = $"UPDATE customer SET cname = '{customer_name.Text}', accno = '{acc_no.Text}', address = '{address_box.Text}'WHERE username = '{username}'";

            }
            int rows = command.ExecuteNonQuery();
              
                    Hide();
                    adminpanel form = new adminpanel();
                    form.ShowDialog();
                    Close();
                
           

            con.Close();


        }

        private void admin_customer_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);
            
        }

        private void user_name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
