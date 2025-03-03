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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace db_projact
{
    public partial class order : Form
    {
        OracleConnection con;
        public string Cid { get; set; }
     
        public string sid { get; set; }
        public string total { get; set; }
        public string username { get; set; }

        public string bill_no { get; set; }

        public order()
        {
            InitializeComponent();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 cart = new Form1();
           
            cart.ShowDialog();
            Close();
        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                OracleCommand command = con.CreateCommand();
                command.CommandText = "DELETE FROM cart";
                int rowsDeleted = command.ExecuteNonQuery();
                this.Hide();
                Hide();
                Form1 form = new Form1();
                form.ShowDialog();
                Close();
            }
            else { MessageBox.Show("error"); }
            
        }


        private void order_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);

            con.Open();
            OracleCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM customer WHERE username = :username ";
            command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

          
            int cid = Convert.ToInt32(command.ExecuteScalar());

            OracleDataAdapter adapter = new OracleDataAdapter("SELECT *  FROM cart ", con);

            DataTable table = new DataTable();

          
            adapter.Fill(table);

            dataGridView2.DataSource = table;
            billno.Text = bill_no;
            c_id.Text = Cid;
           
            s_id.Text = sid;
            t_total.Text = total;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void billno_Click(object sender, EventArgs e)
        {

        }
    }
}
