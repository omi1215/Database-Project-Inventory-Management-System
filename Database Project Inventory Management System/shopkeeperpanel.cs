using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace db_projact
{
    public partial class shopkeeperpanel : Form
    {
        OracleConnection con;
        public shopkeeperpanel()
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

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void shopkeeperpanel_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM product", conStr);
            DataTable table = new DataTable();

           
            adapter.Fill(table);

        
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            con.Open();

          
            OracleCommand command = con.CreateCommand();

        
            command.CommandText = "INSERT INTO PRODUCT (PID, SUPPLIERNAME, CATEGORY, PPRICE, PNAME, PQTY) VALUES (:pid, :suppliername, :category, :pprice, :pname, :pqty)";

           
            command.Parameters.Add("pid", OracleDbType.Varchar2, 100).Value = p_name.Text;
            command.Parameters.Add("suppliername", OracleDbType.Varchar2, 30).Value = s_name.Text;
            command.Parameters.Add("category", OracleDbType.Varchar2, 30).Value = category.Text;
            command.Parameters.Add("pprice", OracleDbType.Decimal, 22).Value = price.Text;
            command.Parameters.Add("pname", OracleDbType.Varchar2, 30).Value = p_name.Text;
            command.Parameters.Add("pqty", OracleDbType.Decimal, 22).Value = qty.Text;

           
            int rowsInserted = command.ExecuteNonQuery();

            MessageBox.Show("inserted the given data");


            
            con.Close();
            Hide();
            shopkeeperpanel form = new shopkeeperpanel();
            form.ShowDialog();
            Close();
        }

        private void button_WOC5_Click(object sender, EventArgs e)
        {

            con.Open();

            
            using (OracleCommand command = con.CreateCommand())
            {
                
                command.CommandText = "UPDATE PRODUCT SET SUPPLIERNAME = :suppliername, CATEGORY = :category, PPRICE = :pprice, PNAME = :pname, PQTY = :pqty WHERE PID = :pid";

              
                command.Parameters.Add("suppliername", OracleDbType.Varchar2, 30).Value = new_supplier.Text;
                command.Parameters.Add("category", OracleDbType.Varchar2, 30).Value = new_category.Text;
                command.Parameters.Add("pprice", OracleDbType.Decimal, 22).Value = new_price.Text;
                command.Parameters.Add("pname", OracleDbType.Varchar2, 30).Value = new_pname.Text;
                command.Parameters.Add("pqty", OracleDbType.Decimal, 22).Value = new_qty.Text;
                command.Parameters.Add("pid", OracleDbType.Varchar2, 100).Value = curr_pid.Text;

              
                int rowsUpdated = command.ExecuteNonQuery();
                MessageBox.Show("updated the given data");
            }

           
            con.Close();
        }

        private void button_WOC3_Click(object sender, EventArgs e)
        {
            con.Open();

           
            OracleCommand command = con.CreateCommand();

          
            command.CommandText = "DELETE FROM PRODUCT WHERE PID = :pid";

           
            command.Parameters.Add("pid", OracleDbType.Varchar2, 100).Value = pid_del.Text;

           
            int rowsDeleted = command.ExecuteNonQuery();
            MessageBox.Show("deleted the given data");
            Hide();
            shopkeeperpanel form = new shopkeeperpanel();
            form.ShowDialog();
            Close();
        }
    }
}
