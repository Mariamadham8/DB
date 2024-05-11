using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace data_base_project
{
    public partial class product2 : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UL6OPPL;Initial Catalog=pet_store;Integrated Security=true");
        SqlDataAdapter sda;
        public product2()
        {
            InitializeComponent();
            display_eymployee();
        }

        private void display_eymployee()
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
            comboBox1.SelectedText = "";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UL6OPPL;Initial Catalog=pet_store;Integrated Security=true"))
                {
                    con.Open();
                    string query = "INSERT INTO products(prod_name, prod_cat ,prod_price,prod_quntity) " +
                                 "VALUES(@prod_name, @prod_cat,@prod_price,@prod_q)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@prod_name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@prod_cat", comboBox1.SelectedItem?.ToString());
                    cmd.Parameters.AddWithValue("@prod_price", decimal.Parse(textBox3.Text));
                    cmd.Parameters.AddWithValue("@prod_q", int.Parse(textBox2.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("product added");
                }

                display_eymployee();
                con.Close();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString());
                if (key == 0)
                {
                    MessageBox.Show("empty file selected");
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UL6OPPL;Initial Catalog=pet_store;Integrated Security=true"))
                    {
                        con.Open();
                        string query = "delete from products  where prod_id =@key";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@key", key);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("product deleted");
                    }
                    display_eymployee();
                    con.Close();
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UL6OPPL;Initial Catalog=pet_store;Integrated Security=true"))
                {
                    con.Open();
                    string query = "update products set prod_name=@prod_name, prod_cat=@prod_cat , prod_price=@prod_price,prod_quntity=@prod_q  where prod_id =@key";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@prod_name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@prod_cat", comboBox1.SelectedItem?.ToString());
                    cmd.Parameters.AddWithValue("@prod_price", decimal.Parse(textBox3.Text));
                    cmd.Parameters.AddWithValue("@prod_q", int.Parse(textBox2.Text));
                    cmd.Parameters.AddWithValue("@key", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString()));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("product updated");
                }
                display_eymployee();
                con.Close();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        int key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (dataGridView1.SelectedRows[0].Cells.Count >= 5)
                    {
                        textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString();
                        textBox2.Text = dataGridView1.SelectedRows[0].Cells[3].Value?.ToString();
                        textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value?.ToString();
                        comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();
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

       
       
        
        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
            billing obj = new billing();
            obj.Show();
            this.Hide();
            
        }

       
        
      
    }

}

