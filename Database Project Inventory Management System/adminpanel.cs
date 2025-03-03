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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace db_projact
{
    public partial class adminpanel : Form
    {
        int sid,cid,phone,accno,salary;
        string cname,username,sname,address,wrkhrs;

        
        OracleConnection con;
        public adminpanel()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button_WOC7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            con.Open();
            OracleCommand command = con.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM record WHERE username = :username";
            command.Parameters.Add("username", OracleDbType.Varchar2).Value = username_add_del.Text;
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count > 0)
            {
                MessageBox.Show("Username already exists. Please choose a different one.");
            }
            else
            {
                string str1="";
                if (radioButton1.Checked)
                {
                    str1 = "shopkeeper";
                }
                else if (radioButton2.Checked)
                {
                    str1 = "customer";
                }
                else
                {
                    MessageBox.Show("Choose between shopkeeper or customer");
                }
                if (radioButton1.Checked || radioButton2.Checked)
                {
                    command = con.CreateCommand();
                    command.CommandText = "INSERT INTO record VALUES (:username, :password, :position)";
                    command.Parameters.Add("username", OracleDbType.Varchar2).Value = username_add_del.Text;
                    command.Parameters.Add("password", OracleDbType.Varchar2).Value = password_add_del.Text;
                    command.Parameters.Add("position", OracleDbType.Varchar2).Value = str1;
                    int rows = command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        if (radioButton1.Checked)
                        {
                            Hide();
                            admin_shopkeeper form = new admin_shopkeeper();
                            form.check = 1;
                            form.username = username_add_del.Text;
                            form.ShowDialog();
                            Close();
                        }
                        else if (radioButton2.Checked)
                        {
                            Hide();
                            admin_customer form = new admin_customer();
                            form.check = 1;
                            form.username = username_add_del.Text;
                            form.ShowDialog();
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while registering. Please try again later.");
                    }
                }
            }

            con.Close();
           
          
        }

        private void button_WOC6_Click(object sender, EventArgs e)
        {
            Hide();
            admin_prod form = new admin_prod();
            form.ShowDialog();
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void adminpanel_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);

            OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM shopkeeper", conStr);
            DataTable table = new DataTable();

           
            adapter.Fill(table);

           
            dataGridView1.DataSource = table;
            adapter = new OracleDataAdapter("SELECT * FROM customer", conStr);
             table = new DataTable();

           
            adapter.Fill(table);

         
            dataGridView2.DataSource = table;
             adapter = new OracleDataAdapter("SELECT * FROM product", conStr);
             table = new DataTable();

          
            adapter.Fill(table);

           
            dataGridView3.DataSource = table;
        }

        private void button_WOC3_Click(object sender, EventArgs e)
        {
            con.Open();
            OracleCommand command = con.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM record WHERE username = :username";
            command.Parameters.Add("username", OracleDbType.Varchar2).Value = username_add_del.Text;
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count > 0)
            {
                string str1 = "";
                if (radioButton1.Checked)
                {
                    str1 = "shopkeeper";
                }
                else if (radioButton2.Checked)
                {
                    str1 = "customer";
                }
                else
                {
                    MessageBox.Show("Choose between shopkeeper or customer");
                }
                if (radioButton1.Checked || radioButton2.Checked)
                {
                    command = con.CreateCommand();
                    command.CommandText = $"DELETE FROM {str1} WHERE username = :username"; 
                    command.Parameters.Add("username", OracleDbType.Varchar2).Value = username_add_del.Text;
                    command.ExecuteNonQuery();

                    command = con.CreateCommand(); 
                    command.CommandText = "DELETE FROM record WHERE username = :username";
                    command.Parameters.Add("username", OracleDbType.Varchar2).Value = username_add_del.Text;
                    command.ExecuteNonQuery();
                    MessageBox.Show("deleted succesfully");
                }
            }
            else
            {
                MessageBox.Show("Username doesn't exist. Please choose a different one.");
            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void button_WOC5_Click(object sender, EventArgs e)
        {
            Hide();
            adminpanel form = new adminpanel();
            form.ShowDialog();
            Close();
        }

        private void button_WOC4_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            Close();
        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            con.Open();
            OracleCommand command = con.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM record WHERE username = :username";
            command.Parameters.Add("username", OracleDbType.Varchar2).Value = username_add_del.Text;
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count == 0)
            {
                MessageBox.Show("Username doesnt exists. Please choose a different one.");
            }
            else
            {
                string str1 = "";
                if (radioButton1.Checked)
                {
                    str1 = "shopkeeper";
                }
                else if (radioButton2.Checked)
                {
                    str1 = "customer";
                }
                else
                {
                    MessageBox.Show("Choose between shopkeeper or customer");
                }
                if (radioButton1.Checked || radioButton2.Checked)
                {
                   
                        if (radioButton1.Checked)
                        {
                            Hide();
                            admin_shopkeeper form = new admin_shopkeeper();
                            form.check = 0;
                        form.username=username_add_del.Text;
                            form.ShowDialog();
                            Close();
                        }
                        else if (radioButton2.Checked)
                        {
                        Hide();
                        admin_customer form = new admin_customer();
                        form.check = 0;
                        form.username = username_add_del.Text;
                        form.ShowDialog();
                        Close();
                        }
                    
                }
            }

            con.Close();

            
        }

    }
}
