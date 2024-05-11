using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace data_base_project
{
    public partial class billing : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UL6OPPL;Initial Catalog=pet_store;Integrated Security=true");
        SqlDataAdapter sda;
        int key = 0;

        public billing()
        {
            InitializeComponent();
            login obje = new login();
            textBox1.Text = obje.t1;
            get_customers();
            display_prod();
        }

        private void display_prod()
        {
            try
            {
                con.Open();
                string quary = "select * from products";
                sda = new SqlDataAdapter(quary, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }
        private void get_customers()
        {
            try
            {
                con.Open();
                string query = "SELECT cus_id FROM customer";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sdr);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "cus_id";
                comboBox1.ValueMember = "cus_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void get_customer_name()
        {
            try
            {
                con.Open();

                string query = "SELECT cus_name FROM customer WHERE cus_id = @cb";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cb", comboBox1.SelectedValue);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox6.Text = reader["cus_name"].ToString();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
           
              
            
        }
        object result;
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Invalid information type");
            }
            else
            {
                
                try
                {
                   
                    con.Open();
                    string query2 = "update products  set cus_id = @cb";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Parameters.AddWithValue("@cb", comboBox1.SelectedValue);
                 
                    cmd2.ExecuteNonQuery();

                    string query = "INSERT INTO cus_order (csu_id, prod_name, prod_q, prod_price,prod_id, total)\r\n" +
                        "SELECT  p.cus_id, p.prod_name, p.prod_quntity, p.prod_price,p.prod_id, p.prod_quntity * p.prod_price AS total\r\n" +
                        "from products p where p.prod_id=@key ;";
                      
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cb", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@key",Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString()));
                    cmd.ExecuteNonQuery();

                    string selectQuery = "SELECT * FROM cus_order";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                    result = comboBox1.SelectedValue;
                  
                   

                    SqlDataReader reader = selectCmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView3.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    clear();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (dataGridView1.SelectedRows[0].Cells.Count >= 4)
                    {
                        textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                        textBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value?.ToString();
                        textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value?.ToString();
                        if (string.IsNullOrEmpty(textBox1.Text))
                        {
                            key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected row doesn't have enough cells.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_customer_name();

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UL6OPPL;Initial Catalog=pet_store;Integrated Security=true"))
                {
                    con.Open();
                    string query3 = "DELETE FROM cus_order";
                    SqlCommand cmd3 = new SqlCommand(query3, con);
                    cmd3.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting cus_order: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UL6OPPL;Initial Catalog=pet_store;Integrated Security=true"))
                {
                    con.Open();
                    string query = "SELECT c.cus_name, c.cus_id, c.cus_phone, c.cus_add, SUM(co.prod_q * co.prod_price) AS total " +
                                   "FROM customer c " +
                                   "JOIN cus_order co ON c.cus_id = co.csu_id " +
                                   "WHERE c.cus_id = @cb " +
                                   "GROUP BY c.cus_name, c.cus_id, c.cus_phone, c.cus_add";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@cb", comboBox1.SelectedValue);

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView2.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving customer data: " + ex.Message);
            }
        }


        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
           
            dataGridView3.DataSource = null;

        }

      
    }
}

