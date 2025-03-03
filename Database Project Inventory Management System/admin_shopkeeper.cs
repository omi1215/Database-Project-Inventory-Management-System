using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace db_projact
{
    public partial class admin_shopkeeper : Form
    {
        OracleConnection con;
        public int check { get; set; }
        public string username{ get; set; }
        public admin_shopkeeper()
        {
            InitializeComponent();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
                con.Open();
                string str = username;
                OracleCommand command = con.CreateCommand();

           
              
                if (check == 1 )
                {
                    command = con.CreateCommand();
                    command.CommandText = "INSERT INTO shopkeeper(username, sname, phone, salary, address, workhrs) VALUES (:username, :sname, :phone, :salary, :address, :workhrs)";
                    command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                    command.Parameters.Add("sname", OracleDbType.Varchar2).Value = shopkeeper_name.Text;
                    command.Parameters.Add("phone", OracleDbType.Decimal).Value = phone_no.Text;
                    command.Parameters.Add("salary", OracleDbType.Decimal).Value = sal.Text;
                    command.Parameters.Add("address", OracleDbType.Varchar2).Value = address_box.Text;
                    command.Parameters.Add("workhrs", OracleDbType.Varchar2).Value = work_hours.Text;

                }
                else if(check == 0 )
                {
                    command = con.CreateCommand();
                    command.CommandText = $"UPDATE shopkeeper SET sname = '{shopkeeper_name.Text}', phone = {phone_no.Text}, salary = {sal.Text}, address = '{address_box.Text}', workhrs = '{work_hours.Text }' WHERE username = '{username}'"; 



                }
                
                    command.ExecuteNonQuery();
                    
                        Hide();
                        adminpanel form = new adminpanel();
                        form.ShowDialog();
                        Close();
                    
                

                con.Close();
            

        }

        private void admin_shopkeeper_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);
      
        }
    }
}
