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
using Oracle.ManagedDataAccess.Types;

namespace db_projact
{
    public partial class cart : Form
    {
        OracleConnection con;
        public string username { get; set; }
        public int shopid { get; set; }
        public int cid ;
        public int billno;
        public int sid;
        public int total;
        public int ccid;
        public cart()
        {
            InitializeComponent();
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            
                OracleCommand cmd = con.CreateCommand();

                cmd.CommandText = "INSERT INTO order_main (cid, sid) VALUES (:cid, :o_sid)";

                cmd.Parameters.Add("cid", OracleDbType.Int64).Value = ccid;
                cmd.Parameters.Add("o_sid", OracleDbType.Int64).Value = shopid;

                int rowsInserted = cmd.ExecuteNonQuery();

                cmd = con.CreateCommand();
           
            cmd.CommandText = $"INSERT INTO order_b (billno, pid, oqty, oprice, tprice) SELECT (SELECT max(billno) FROM order_main WHERE cid in {ccid} AND sid in {shopid} ), pid, qty, 100, 900 FROM cart";

    

                rowsInserted = cmd.ExecuteNonQuery();

                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT billno , sid FROM order_main WHERE cid = :cid AND sid = :o_sid";
                cmd.Parameters.Add("cid", OracleDbType.Decimal).Value = ccid;
            cmd.Parameters.Add("o_sid", OracleDbType.Decimal).Value = shopid;
            using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        billno = reader.GetInt32(0);
                        sid = reader.GetInt32(1);

                    }
                }


            cmd.CommandText = "SELECT calculate_total_sum() FROM DUAL";

        
            decimal totalSum = (decimal)cmd.ExecuteScalar();

            this.Hide();
                Hide();
                order order = new order();
                order.username = username;
                order.sid = Convert.ToString( sid);
                order.total =Convert.ToString( totalSum);
                order.bill_no = Convert.ToString(billno);
                order.Cid = Convert.ToString(ccid);
                order.ShowDialog();
                Close();
          
        }


        private void button_WOC3_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Hide();
            product product = new product();
            product.username = username;
            product.shopid = shopid;
            product.ShowDialog();
            Close();
        }

        private void cart_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=ROSHAAN;PASSWORD=123";
            con = new OracleConnection(conStr);
          


            con.Open();
            OracleCommand command = con.CreateCommand();
            command.CommandText = "SELECT cid FROM customer WHERE username = :username ";
            command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

            // Cast the result of ExecuteScalar to an integer
            int cid = Convert.ToInt32(command.ExecuteScalar());

            OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM cart where cid = :cid ", con);
            adapter.SelectCommand.Parameters.Add("cid", OracleDbType.Decimal).Value = cid;
            DataTable table = new DataTable();

            // Fill the DataTable with data from the Oracle table
            adapter.Fill(table);

            dataGridView1.DataSource = table;
        

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            OracleCommand command = con.CreateCommand();
            command.CommandText = "delete  FROM cart WHERE pid = :pid ";
            command.Parameters.Add("pid", OracleDbType.Varchar2).Value = pid.Text;
            command.ExecuteNonQuery();
            MessageBox.Show("item deleted");
            this.Hide();
            Hide();
            cart cart = new cart();
            cart.username = username;
            cart.ShowDialog();
            Close();
        }
    }
}
