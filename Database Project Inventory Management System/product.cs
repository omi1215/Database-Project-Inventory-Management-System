using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace db_projact
{
    
    public partial class product : Form
    {
        OracleConnection con;
        public string username { get; set; }
        public int shopid{ get; set; }
        int cid ;
        public product()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
        
            string username1 = username;
            this.Hide();
            Hide();
            cart cart = new cart();
            cart.username = username1;
            cart.shopid = shopid;   
            cart.ccid= cid;
            cart.ShowDialog();
            Close();
        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form = new Form1();
            form.ShowDialog();
            Close();
        }

        private void product_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT pid , pname , category , pprice FROM product ", con);

            DataTable table = new DataTable();


            adapter.Fill(table);

            dataGridView1.DataSource = table;

            dataGridView2.Columns.Add("CID", "CID");

            con.Open();
            OracleCommand command = con.CreateCommand();
            command.CommandText = "SELECT cid FROM customer WHERE username = :username ";
            command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

           
             cid = Convert.ToInt32(command.ExecuteScalar());

   
            int rowIndex = dataGridView2.Rows.Add();
            dataGridView2.Rows[rowIndex].Cells[0].Value = cid;
          
        
        }

        private void category_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button_WOC4_Click(object sender, EventArgs e)
        {
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM product ", con);

            DataTable table = new DataTable();

         
            adapter.Fill(table);
            if (category == null)
            { MessageBox.Show("category box is null"); }
            else
            {
              
                dataGridView1.DataSource = table;
                DataView dv = table.DefaultView;
                dv.RowFilter = string.Format("category LIKE '%{0}%'", category.Text);
                dataGridView1.DataSource = dv.ToTable();
            }
        
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_WOC3_Click(object sender, EventArgs e)
        {
           
            string str1;
            int count = 0;

            OracleCommand command = con.CreateCommand();
            command.CommandText = "select pid from cart where pid = :pid";
            command.Parameters.Add("pid", OracleDbType.Varchar2).Value = pid.Text;
            str1 = Convert.ToString(command.ExecuteScalar());

            if (str1 == Convert.ToString(pid.Text))
            {
                command = con.CreateCommand();
                command.CommandText = "select qty from cart where pid = :pid";
                command.Parameters.Add("pid", OracleDbType.Varchar2).Value = pid.Text;
                count= Convert.ToInt32(command.ExecuteScalar());
                int sum = Convert.ToInt32(qty.Text) + count;
              
                command = con.CreateCommand();
                command.CommandText = "UPDATE cart SET qty=:new WHERE pid=:pid ";
                command.Parameters.Add("new", OracleDbType.Decimal).Value = sum;
      
                command.Parameters.Add("pid", OracleDbType.Varchar2, 100).Value = pid.Text;

                int rowsInserted = command.ExecuteNonQuery();
            }
            else
            {
                command = con.CreateCommand();
                command.CommandText = "select count(pid) from product where pid = :pid";
                command.Parameters.Add("pid", OracleDbType.Varchar2).Value = pid.Text;
                int row_check = Convert.ToInt32(command.ExecuteScalar());

                if (row_check > 0)
                {
                    command = con.CreateCommand();
                    command.CommandText = "INSERT INTO CART (PID, CID, QTY) VALUES (:pid, :cid, :qty)";


                    command.Parameters.Add("pid", OracleDbType.Varchar2).Value = pid.Text;
                    command.Parameters.Add("cid", OracleDbType.Decimal).Value = cid;
                    command.Parameters.Add("qty", OracleDbType.Decimal).Value = qty.Text;


                    int rowsInserted = command.ExecuteNonQuery();
                    MessageBox.Show("Inserted.");
                }
                else { MessageBox.Show("incorrect code for item has been inserted"); }
            }

          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
