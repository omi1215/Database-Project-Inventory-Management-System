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

    public partial class login : Form
    {
        
        OracleConnection con;
        public login()
        {
            InitializeComponent();

        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            con.Open();
            OracleCommand command = con.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM record WHERE username = :username AND password = :password";
            command.Parameters.Add("username", OracleDbType.Varchar2).Value = user_name.Text;
            command.Parameters.Add("password", OracleDbType.Varchar2).Value = pass_word.Text;
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count > 0)
            {
                command = con.CreateCommand();
                command.CommandText = "SELECT position FROM record WHERE username = :username AND password = :password";
                command.Parameters.Add("username", OracleDbType.Varchar2).Value = user_name.Text;
                command.Parameters.Add("password", OracleDbType.Varchar2).Value = pass_word.Text;
                string str = Convert.ToString(command.ExecuteScalar());

                if (str == "customer")
                {
                    command = con.CreateCommand();
                    command.CommandText = "SELECT max(sid) FROM shopkeeper";
                    int str1 = Convert.ToInt32(command.ExecuteScalar());
                    if (str1 > -1)
                    {
                        string username = user_name.Text;
                        this.Hide();
                        Hide();
                        product product = new product();
                        product.username = username;
                        product.shopid = str1;
                        product.ShowDialog();
                        Close();
                    }
                    else { MessageBox.Show("there is no available shopkeeper"); }
                }
                else if (str == "shopkeeper")
                {
                    Hide();
                    shopkeeperpanel form = new shopkeeperpanel();
                    form.ShowDialog();
                    Close();
                }
                else if (str == "admin")
                {
                    Hide();
                    adminpanel form = new adminpanel();
                    form.ShowDialog();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Username or password is incorrect. Please try again.");
            }
            con.Close();
        }


        private void login_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);
        }
    }
}
